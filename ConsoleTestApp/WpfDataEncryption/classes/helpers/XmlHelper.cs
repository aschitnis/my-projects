using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

/// <summary>
/// siehe Link : https://jonathancrozier.com/blog/xml-serialization-with-c-sharp
/// </summary>
namespace WpfDataEncryption.classes.helpers
{
    public static class XmlHelper
    {
        internal static T DeserializeFromString<T>(string xml) where T : class
        {
            using (var reader = new StringReader(xml))
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(T));

                    T xmlobject = (T)serializer.Deserialize(reader);
                    return xmlobject;
                }
                catch(Exception e)
                {
                    throw e;
                }
            }
        }

        internal static string SerializeToString<T>(T objectToSerialize) where T : class
        {
            string xmldataString = null;
            StringWriter writer = new StringWriter();
            try
            {
                // Do this to avoid the serializer inserting default XML namespaces.
                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add(string.Empty, string.Empty);

                var serializer = new XmlSerializer(objectToSerialize.GetType());
                serializer.Serialize(writer, objectToSerialize, namespaces);
                xmldataString = writer.ToString();
                writer.Dispose();
                return xmldataString;
            }
            catch(Exception ex)
            {
                if (writer != null)
                    writer.Dispose();
                throw ex;
            }
        }
    }
}
