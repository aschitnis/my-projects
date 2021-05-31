using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WpfDataEncryption.classes.helpers;
using WpfDataEncryption.models;

/// <summary>
/// This class encapsulates/calls the serialize & deserialize functions from the XmlHelper static class. 
/// The class is implemented as a Singleton pattern because only 1 instance of the Xml data has to exist in memory whilst the application is running.
/// </summary>
namespace WpfDataEncryption.classes
{
    public class XmlSingletonManager
    {
        private static XmlSingletonManager _instance;
        private XmlObjectModel xmldata;
        private string XmlSerializedString { get; set; }
        public XmlObjectModel XmlData
        {
            get
            {
                if (xmldata == null)
                    xmldata = new XmlObjectModel();
                return xmldata;
            }
        }

        private XmlSingletonManager() 
        {
            xmldata = new XmlObjectModel();
        }

        // Lock synchronization object
        private static object syncLock = new object();

        public static XmlSingletonManager GetXmlManager()
        {
            // Support multithreaded applications through 'Double checked locking' pattern which (once the instance exists) avoids locking each time the method is invoked
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new XmlSingletonManager();
                    }
                }
            }
            return _instance;
        }

        /// <summary>
        /// a) Encrypt the xml data and save it to a xml file.
        /// b) decrypt the xml data. 
        /// c) deserialize the xml data(String) to a object.
        /// </summary>
        /// <param name="xml">the xml string data</param>
        /// <returns>Exception object. If no errors then the exception Object return NULL</returns>
        internal Exception DeserializeXmlToDataObject(string xml)
        {
            try
            {
               // xml = File.ReadAllText(PathManager.FILE_Data_Plaintext, Constants.DEFAULT_FILE_ENCODING);

                Security.EncryptStringToFile(PathManager.FILE_Data_Encrypted, xml, PasswordManager.GetPersonalDatabaseEncryptionPassword(), Constants.DEFAULT_FILE_ENCODING, Constants.PERSONAL_DATA_ENCRYPTION_SALT, Constants.ENCRYPTION_INIT_VECTOR);
                xml = Security.DecryptFileToString(PathManager.FILE_Data_Encrypted, PasswordManager.GetPersonalDatabaseEncryptionPassword(), Constants.DEFAULT_FILE_ENCODING, Constants.PERSONAL_DATA_ENCRYPTION_SALT, Constants.ENCRYPTION_INIT_VECTOR);

                xmldata = XmlHelper.DeserializeFromString<XmlObjectModel>(xml);
                return null;
            }
            catch(Exception e)
            {
                xmldata = null;
                return e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal Exception DeserializeEncryptedXmlToDataObject()
        {
            string xml = null;
            Exception ex = null;

            try
            {
                xml = Security.DecryptFileToString(PathManager.FILE_Data_Encrypted, PasswordManager.GetPersonalDatabaseEncryptionPassword(), Constants.DEFAULT_FILE_ENCODING, Constants.PERSONAL_DATA_ENCRYPTION_SALT, Constants.ENCRYPTION_INIT_VECTOR);
                xmldata = XmlHelper.DeserializeFromString<XmlObjectModel>(xml);
                return null;
            }
            catch (Exception e)
            {
                xmldata = null;
                //xml = Security.DecryptFileToString(PathManager.FILE_Data_Encrypted, PasswordManager.GetPersonalDatabaseEncryptionPassword(), Encoding.Default, Constants.PERSONAL_DATA_ENCRYPTION_SALT, Constants.ENCRYPTION_INIT_VECTOR);
                return e;
            }
        }

        /// <summary>
        /// Serialize the Xml-Object to a String
        /// </summary>
        /// <param name="objectToSerialize">XML object to be serialized into a String</param>
        /// <returns>Exception object. If no errors then the exception Object return NULL</returns>
        internal Exception SerializeXml(XmlObjectModel objectToSerialize)
        {
            try
            {
                XmlSerializedString = XmlHelper.SerializeToString<XmlObjectModel>(objectToSerialize);
            }
            catch(Exception e)
            {
                XmlSerializedString = null;
                return e;
            }
            return null;
        }

        internal XmlObjectModel GetXmlObject()
        {
            return xmldata;
        }

        internal XmlObjectModel GetXmlClonedObject()
        {
            return (XmlObjectModel)xmldata.Clone();
        }
    }
}
