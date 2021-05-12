using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WpfDataEncryption.classes
{
    public static class Security
    {
        public static string DecryptFileToString(string encryptedFile, string passPhrase, Encoding encoding, string salt = "asdk23904uasdfji", byte[] initVector = null)
        {
            if (string.IsNullOrEmpty(encryptedFile) || !File.Exists(encryptedFile))
                return null;

            if (initVector == null)
                return null;

            byte[] saltValueBytes = Encoding.ASCII.GetBytes(salt);
            byte[] cipherTextBytes;

            //read file
            using (StreamReader sr = new StreamReader(encryptedFile, encoding, true))
            {
                string cipherText = sr.ReadToEnd();
                cipherTextBytes = Convert.FromBase64String(cipherText);
            }

            Rfc2898DeriveBytes passwordBytes = new Rfc2898DeriveBytes(passPhrase, saltValueBytes, 1000);

            byte[] keyBytes = passwordBytes.GetBytes(256 / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVector);

            // Define memory stream which will be used to hold encrypted data and a cryptographic stream (always use Read mode for encryption)
            byte[] plainTextBytes;
            int decryptedByteCount;
            using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
            {
                // Since at this point we don't know what the size of decrypted data will be, allocate the buffer long enough to hold ciphertext; plaintext is never longer than ciphertext.
                plainTextBytes = new byte[cipherTextBytes.Length];
                decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            }

            //Convert decrypted data into a string
            string plainText = encoding.GetString(plainTextBytes, 0, decryptedByteCount);

            return plainText;
        }

        public static void EncryptStringToFile(string destinationEncryptedFilePath, string plainText, string passPhrase, Encoding encoding, string salt = "asdk23904uasdfji", byte[] initVector = null)
        {
            if (string.IsNullOrEmpty(destinationEncryptedFilePath) || string.IsNullOrEmpty(passPhrase))
                return;

            if (initVector == null)
                return;

            byte[] saltValueBytes = Encoding.ASCII.GetBytes(salt);
            byte[] plainTextBytes = encoding.GetBytes(plainText);

            Rfc2898DeriveBytes passwordBytes = new Rfc2898DeriveBytes(passPhrase, saltValueBytes, 1000);

            byte[] keyBytes = passwordBytes.GetBytes(256 / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVector);
            byte[] cipherTextBytes;

            // Define memory stream which will be used to hold encrypted data and a cryptographic stream (always use Write mode for encryption).
            using (MemoryStream memoryStream = new MemoryStream())
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                cipherTextBytes = memoryStream.ToArray();
            }

            // Convert encrypted data into a base64-encoded string
            string cipherText = Convert.ToBase64String(cipherTextBytes);

            //save the new file under a temporary name, then replace the original file with it (safer saving ;)
            string filePathTmp = destinationEncryptedFilePath + "_"+ DateTime.Now.Ticks + ".tmp";

            //save the file and tell the OS to write it directly without any caching
            byte[] data = encoding.GetBytes(cipherText);

            using (FileStream fs = new FileStream(filePathTmp, FileMode.Create, FileAccess.Write, FileShare.Read, data.Length, FileOptions.WriteThrough))
            {
                fs.Write(data, 0, data.Length);
            }

            //now replace the old file with the new one
            if (File.Exists(destinationEncryptedFilePath))
                File.Delete(destinationEncryptedFilePath);

            File.Move(filePathTmp, destinationEncryptedFilePath);
        }
    }
}
