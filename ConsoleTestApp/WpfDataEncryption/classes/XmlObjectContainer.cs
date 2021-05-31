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
            XmlDeserializedModel = XmlSingletonManager.GetXmlManager().XmlData;
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
            XmlSingletonManager singletonXmlManager = XmlSingletonManager.GetXmlManager();

            if (File.Exists(PathManager.FILE_Data_Encrypted) == true)   // code for encrypted Xml file.
            {
                ex = singletonXmlManager.DeserializeEncryptedXmlToDataObject();
                
                if (ex == null)
                {
                    XmlDeserializedModel = singletonXmlManager.GetXmlObject();
                }
                return ex;
            }
            else if (File.Exists(PathManager.FILE_Data_Plaintext) == true)  // code for Plaintext Xml file.
            {
                string xml = File.ReadAllText(PathManager.FILE_Data_Plaintext, Constants.DEFAULT_FILE_ENCODING);
                ex = singletonXmlManager.DeserializeXmlToDataObject(xml);   // deserialize xml string into object

                // if a exception is thrown then check if a encrypted file exists. If YES then delete .AES file.
                if (ex != null)
                {
                    if (File.Exists(PathManager.FILE_Data_Encrypted))
                        File.Delete(PathManager.FILE_Data_Encrypted);
                }
                // If there were no Exceptions found, get the deserialized Xml Object, then delete the plaintext file.
                if (ex == null)
                {
                    XmlDeserializedModel = singletonXmlManager.GetXmlObject();
                    if (File.Exists(PathManager.FILE_Data_Plaintext))
                        File.Delete(PathManager.FILE_Data_Plaintext);
                }
                return ex;
            }
            else
            {
                return new Exception("Neither *.aes nor *.xml file exists!. The Processing is terminated.");
            }
        }
    }
}
