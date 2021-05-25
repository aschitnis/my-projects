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
/// </summary>
namespace WpfDataEncryption.classes
{
    internal sealed class XmlManager
    {
        public XmlObjectModel XmlDeserializedObject { get; set; }
        private string XmlSerializedString { get; set; }

        public XmlManager() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml">the xml string to be deserialized into a object</param>
        /// <returns>Exception object. If no errors then the exception Object return NULL</returns>
        internal Exception DeserializeXml(string xml)
        {
            try
            {
                XmlDeserializedObject = XmlHelper.DeserializeFromString<XmlObjectModel>(xml);
                return null;
            }
            catch(Exception e)
            {
                XmlDeserializedObject = null;
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
            return XmlDeserializedObject;
        }
        internal XmlObjectModel GetXmlClonedObject()
        {
            return (XmlObjectModel)XmlDeserializedObject.Clone();
        }
    }
}
