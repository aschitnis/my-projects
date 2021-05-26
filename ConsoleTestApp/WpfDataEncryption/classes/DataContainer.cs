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
    internal class ModelsContainer : ViewModelBase
    {
        private List<TopicModel> _topics;
        private List<TopicModel> _filteredtopics;
       // private XmlManager xmlDataManager { get; } = new XmlManager();
        private XmlObjectModel xmlModel { get; set; }

        public List<TopicModel> Topics
        { get => _topics ?? new List<TopicModel>(); private set => SetProperty(ref _topics, value); }
        public List<TopicModel> FilteredTopics { get => _filteredtopics ?? new List<TopicModel>(); set => SetProperty(ref _filteredtopics, value); }
        
        public ModelsContainer() 
        {
            xmlModel = XmlManager.GetXmlManager().XmlData;
        }

        /// <summary>
        /// 1) deserialize xml string into a object
        /// 2) get the xml object data.
        /// </summary>
        /// <param name="isencryptedfileexists">whether only a encrypted version of the xml file exists (*.aes)</param>
        /// <param name="nofilefound"></param>
        /// <returns></returns>
        private Exception GetXmlData(bool isencryptedfileexists, bool nofilefound = true)
        {
            if (nofilefound)
                return new Exception("Neither *.aes nor *.xml file exists!. The Processing is terminated.");

            Exception ex = null;
            if (isencryptedfileexists)
                ex = DeserializeEncryptedXmlToDataObject(); // decrypt xml string & it deserialize into object
            else ex = DeserializeXmlToDataObject();        // deserialize xml string into object

            if (ex != null)
                return ex;

            xmlModel = XmlManager.GetXmlManager().XmlData; //xmlDataManager.GetXmlClonedObject();
            return null;
        }

        private void Fill()
        {

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
            xml = Security.DecryptFileToString(PathManager.FILE_Data_Encrypted, PasswordManager.GetPersonalDatabaseEncryptionPassword(), encoding, Constants.PERSONAL_DATA_ENCRYPTION_SALT, Constants.ENCRYPTION_INIT_VECTOR);

            ex = XmlManager.GetXmlManager().DeserializeXml(xml);
            return ex;
        }
    }
}
