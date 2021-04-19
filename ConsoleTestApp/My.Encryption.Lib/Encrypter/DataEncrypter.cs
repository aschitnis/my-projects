using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using System.Configuration;
using My.Encryption.Interfaces;
using My.Encryption.Extensions;

namespace My.Encryption.Encryptor
{
    public class DataEncrypter : IAllEncryptionTypes
    {
        public string Password { get; private set; }
        #region interface implementations
        public string HashedData { get; set; }
        bool IEncryptionString.EncryptString(string text)
        {
            if (text.HasValue())
                Password = text;

            HashedData = BCrypt.Net.BCrypt.HashPassword(Password);
            return true;
        }

        bool ISaltEncryptionString.EncryptString(string text)
        {
            if (text.HasValue())
                Password = text;

            string saltString = BCrypt.Net.BCrypt.GenerateSalt(12);
            HashedData = BCrypt.Net.BCrypt.HashPassword(Password, saltString);
            return true;
        }
        #endregion

        public bool EncryptString(EncryptionType encryptiontype, string text = null)
        {
            bool issuccess = false;

            switch (encryptiontype.Value)
            {
                case 0:
                    issuccess = ((IEncryptionString)this).EncryptString(text);
                    break;
                case 1:
                    issuccess = ((ISaltEncryptionString)this).EncryptString(text);
                    break;
            }
            return issuccess;
        }
        
        public bool ValidatePassword(string password, string correcthash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correcthash);
        }

        #region constructors
        /// <summary>
        /// i) when no password to-be-encrypted is passed as a (optional)parameter, 
        ///     then the default password from App.config is read & encrypted using the basic encryption algorithm.
        /// ii) when password to-be-encrypted is passed as a (optional)parameter,
        ///     then this password is saved to the App.config file.
        /// </summary>
        /// <param name="password">explicit password is optional</param>
        public DataEncrypter(string password = null)  
        {
            if (password.HasValue())
                Password = password;
        }
        #endregion
    }
}
