using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDataEncryption.classes;

namespace WpfDataEncryption.viewmodels
{
    public class DataEncryptionViewModel
    {
        private bool isEncryptedFileExists = true;
        private bool isPlainTextFileExists = true;

        public DataEncryptionViewModel()
        {
            isEncryptedFileExists = File.Exists(PathManager.FILE_Data_Encrypted);
            isPlainTextFileExists = File.Exists(PathManager.FILE_Data_Plaintext);

            Encoding encoding = Constants.DEFAULT_FILE_ENCODING;
            string encryptedfilepath = PathManager.FILE_Data_Encrypted;
            string destinationfilepath = PathManager.FILE_Data_Plaintext;
            string xml = null;

            if (isEncryptedFileExists)
            {
                // decrypt file
                try
                {
                    xml = Security.DecryptFileToString(encryptedfilepath, PasswordManager.GetPersonalDatabaseEncryptionPassword(), encoding, Constants.PERSONAL_DATA_ENCRYPTION_SALT, Constants.ENCRYPTION_INIT_VECTOR);
                }
                catch
                {
                    xml = Security.DecryptFileToString(encryptedfilepath, PasswordManager.GetPersonalDatabaseEncryptionPassword(), Encoding.Default, Constants.PERSONAL_DATA_ENCRYPTION_SALT, Constants.ENCRYPTION_INIT_VECTOR);
                }
            }
            else
            {
                if (isPlainTextFileExists)
                {
                    // encrypt 
                    // delete plaintextfile
                    try
                    {
                        xml = File.ReadAllText(destinationfilepath, encoding);

                        Security.EncryptStringToFile(encryptedfilepath, xml, PasswordManager.GetPersonalDatabaseEncryptionPassword(), Constants.DEFAULT_FILE_ENCODING, Constants.PERSONAL_DATA_ENCRYPTION_SALT, Constants.ENCRYPTION_INIT_VECTOR);
                        xml = Security.DecryptFileToString(encryptedfilepath, PasswordManager.GetPersonalDatabaseEncryptionPassword(), encoding, Constants.PERSONAL_DATA_ENCRYPTION_SALT, Constants.ENCRYPTION_INIT_VECTOR);
                        
                        XmlManager xmlManager = new XmlManager();
                        Exception ex = xmlManager.DeserializeXml(xml);

                        var original = xmlManager.GetXmlObject();
                        var o = xmlManager.GetXmlClonedObject();
                        o.TopicsList.Clear();

                     }
                    catch (Exception e)
                    {

                    }
                }
                else
                {
                    // error message ! Neither found encrypted File nor the Plaintext File.
                    isEncryptedFileExists = false;
                    isPlainTextFileExists = false;
                }
            }
        }

        private Exception EncryptFile()
        {
            Exception ex = null;
            return ex;
        }
    }
}
