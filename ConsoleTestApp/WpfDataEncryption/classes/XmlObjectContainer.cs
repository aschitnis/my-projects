using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfDataEncryption.viewmodels;
using WpfDataEncryption.models;
using System.Threading.Tasks;
using System.IO;

namespace WpfDataEncryption.classes
{
    internal class XmlObjectContainer : ViewModelBase
    {
        private List<TopicModel> _topics;
        private List<TopicModel> _filteredtopics;

        public XmlObjectModel XmlDeserializedModel { get; private set; }

        public List<TopicModel> Topics
        { get => _topics ?? new List<TopicModel>(); private set => SetProperty(ref _topics, value); }
        public List<TopicModel> FilteredTopics { get => _filteredtopics ?? new List<TopicModel>(); set => SetProperty(ref _filteredtopics, value); }
        
        public XmlObjectContainer() 
        {
            XmlDeserializedModel = XmlManager.GetXmlManager().XmlData;
        }

        /// <summary>
        /// 1) check if the encrypted or plaintext xml file exists under the path.
        /// 2) deserialize xml string into a object
        /// 3) get the xml object data.
        /// </summary>
        /// <param name="isencryptedfileexists">whether only a encrypted version of the xml file exists (*.aes)</param>
        /// <param name="nofilefound"></param>
        /// <returns></returns>
        public Exception GetData()
        {
            Exception ex = null;
            if (File.Exists(PathManager.FILE_Data_Encrypted) == true)
            {
                ex = DeserializeEncryptedXmlToDataObject(); // decrypt xml string & it deserialize into object
            }
            else if (File.Exists(PathManager.FILE_Data_Plaintext) == true)
            {
                ex = DeserializeXmlToDataObject();        // deserialize xml string into object
            }
            else
            {
                return new Exception("Neither *.aes nor *.xml file exists!. The Processing is terminated.");
            }

            if (ex != null)
                return ex;

            XmlDeserializedModel = XmlManager.GetXmlManager().XmlData; //xmlDataManager.GetXmlClonedObject();

            return null;
        }

        private Exception DeserializeEncryptedXmlToDataObject()
        {
            string xml = null;
            Exception ex = null;

            try
            {
                xml = Security.DecryptFileToString(PathManager.FILE_Data_Encrypted, PasswordManager.GetPersonalDatabaseEncryptionPassword(), Constants.DEFAULT_FILE_ENCODING, Constants.PERSONAL_DATA_ENCRYPTION_SALT, Constants.ENCRYPTION_INIT_VECTOR);
            }
            catch(Exception e)
            {
                xml = Security.DecryptFileToString(PathManager.FILE_Data_Encrypted, PasswordManager.GetPersonalDatabaseEncryptionPassword(), Encoding.Default, Constants.PERSONAL_DATA_ENCRYPTION_SALT, Constants.ENCRYPTION_INIT_VECTOR);
                return e;
            }

            ex = XmlManager.GetXmlManager().DeserializeXml(xml);
            return ex;
        }
        /// <summary>
        /// The xml file is in plaintext (*.xml). 
        /// a) Read the file into a String, encrypt the String & save the String to a file (*.aes).
        /// b) Decrypt the encrypted file & read the data into a String.
        /// c) Deserialize the (Xml)String to a Object.
        /// </summary>
        /// <returns></returns>
        private Exception DeserializeXmlToDataObject()
        {
            Exception ex = null;
            string xml = File.ReadAllText(PathManager.FILE_Data_Plaintext, Constants.DEFAULT_FILE_ENCODING);

            Security.EncryptStringToFile(PathManager.FILE_Data_Encrypted, xml, PasswordManager.GetPersonalDatabaseEncryptionPassword(), Constants.DEFAULT_FILE_ENCODING, Constants.PERSONAL_DATA_ENCRYPTION_SALT, Constants.ENCRYPTION_INIT_VECTOR);
            xml = Security.DecryptFileToString(PathManager.FILE_Data_Encrypted, PasswordManager.GetPersonalDatabaseEncryptionPassword(), Constants.DEFAULT_FILE_ENCODING, Constants.PERSONAL_DATA_ENCRYPTION_SALT, Constants.ENCRYPTION_INIT_VECTOR);

            ex = XmlManager.GetXmlManager().DeserializeXml(xml);
            if (ex != null)
                File.Delete(PathManager.FILE_Data_Encrypted);
            return ex;
        }
    }
}
