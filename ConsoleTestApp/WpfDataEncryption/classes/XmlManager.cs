using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WpfDataEncryption.classes.helpers;

/// <summary>
/// This class encapsulates/calls the serialize & deserialize functions from the XmlHelper static class. 
/// The class is implemented as a Singleton pattern because only 1 instance of the Xml data has to exist in memory whilst the application is running.
/// </summary>
namespace WpfDataEncryption.classes
{
    public class XmlManager
    {
        private static XmlManager _instance;
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

        private XmlManager() 
        {
            xmldata = new XmlObjectModel();
        }

        // Lock synchronization object
        private static object syncLock = new object();

        public static XmlManager GetXmlManager()
        {
            // Support multithreaded applications through 'Double checked locking' pattern which (once the instance exists) avoids locking each time the method is invoked
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new XmlManager();
                    }
                }
            }
            return _instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml">the xml string to be deserialized into a object</param>
        /// <returns>Exception object. If no errors then the exception Object return NULL</returns>
        internal Exception DeserializeXml(string xml)
        {
            try
            {
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

        //internal XmlObjectModel GetXmlObject()
        //{
        //    return xmldata;
        //}
        internal XmlObjectModel GetXmlClonedObject()
        {
            return (XmlObjectModel)xmldata.Clone();
        }
    }
}
