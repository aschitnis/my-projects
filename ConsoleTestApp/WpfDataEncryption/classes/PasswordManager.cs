using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDataEncryption.Properties;

namespace WpfDataEncryption.classes
{
    public abstract class PasswordManager
    {
        internal static string GetPersonalDatabaseEncryptionPassword()
        {
            if (string.IsNullOrEmpty(Settings.Default.PersonalDataEncryptionPassword))
            {
                return Constants.PERSONAL_DATA_ENCRYPTION_DEFAULT_PASSWORD;
            }
            return Constants.PERSONAL_DATA_ENCRYPTION_DEFAULT_PASSWORD;
        }
    }
}
