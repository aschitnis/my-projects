using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfDataEncryption.viewmodels;
using WpfDataEncryption.models;
using System.Threading.Tasks;

namespace WpfDataEncryption.classes
{
    internal class ModelsContainer : ViewModelBase
    {
        private List<TopicModel> _topics;
        private List<TopicModel> _filteredtopics;
        private XmlManager xmlDataManager { get; } = new XmlManager();
        private XmlObjectModel xmlModel { get; set; }

        public List<TopicModel> Topics
        { get => _topics ?? new List<TopicModel>(); private set => SetProperty(ref _topics, value); }
        public List<TopicModel> FilteredTopics { get => _filteredtopics ?? new List<TopicModel>(); set => SetProperty(ref _filteredtopics, value); }
        
        public ModelsContainer() { }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isencryptedfileexists"></param>
        /// <param name="nofilefound"></param>
        /// <returns></returns>
        private Exception Initialize(bool isencryptedfileexists, bool nofilefound = true)
        {
            if (nofilefound)
                return new Exception("Neither *.aes nor *.xml file exists!. The Processing is terminated.");

            Exception ex = null;
            ex = isencryptedfileexists == true : DeserializeEncryptedXmlToDataObject() ? DeserializeXmlToDataObject();

            if (ex != null)
                return ex;

            xmlModel = xmlDataManager.GetXmlClonedObject();
        }

        private void Fill()
        {

        }

        private Exception DeserializeEncryptedXmlToDataObject()
        {
            string xml = null;

            try
            {
                xml = Security.DecryptFileToString(PathManager.FILE_Data_Encrypted, PasswordManager.GetPersonalDatabaseEncryptionPassword(), Constants.DEFAULT_FILE_ENCODING, Constants.PERSONAL_DATA_ENCRYPTION_SALT, Constants.ENCRYPTION_INIT_VECTOR);
            }
            catch
            {
                xml = Security.DecryptFileToString(encryptedfilepath, PasswordManager.GetPersonalDatabaseEncryptionPassword(), Encoding.Default, Constants.PERSONAL_DATA_ENCRYPTION_SALT, Constants.ENCRYPTION_INIT_VECTOR);
            }

            ex = xmlDataManager.DeserializeXml(xml);
            return ex;
        }

        private Exception DeserializeXmlToDataObject()
        {
            Exception ex = null;
            string xml = File.ReadAllText(destinationfilepath, encoding);

            Security.EncryptStringToFile(PathManager.FILE_Data_Encrypted, xml, PasswordManager.GetPersonalDatabaseEncryptionPassword(), Constants.DEFAULT_FILE_ENCODING, Constants.PERSONAL_DATA_ENCRYPTION_SALT, Constants.ENCRYPTION_INIT_VECTOR);
            xml = Security.DecryptFileToString(PathManager.FILE_Data_Encrypted, PasswordManager.GetPersonalDatabaseEncryptionPassword(), encoding, Constants.PERSONAL_DATA_ENCRYPTION_SALT, Constants.ENCRYPTION_INIT_VECTOR);

            ex = xmlDataManager.DeserializeXml(xml);
            return ex;
        }
    }
}
