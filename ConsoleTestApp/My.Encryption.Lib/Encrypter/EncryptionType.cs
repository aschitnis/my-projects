using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Encryption.Encryptor
{
    public class EncryptionType
    {
        public static EncryptionType Hashing { get; } = new EncryptionType(0,"Hashing");
        public static EncryptionType HashWithSalt { get; } = new EncryptionType(1, "HashWithSalt");
        public string Name { get; private set; }
        public int Value { get; private set; }
        private EncryptionType(int value,string name)
        {
            Name = name;
            Value = value;
        }
        public static IEnumerable<EncryptionType> List()
        {
            return new[] { Hashing, HashWithSalt };
        }
        public static EncryptionType FromString(string encryptiontypename)
        {
            return List().Single(e => String.Equals(e.Name, encryptiontypename, StringComparison.OrdinalIgnoreCase));
        }
        public static EncryptionType FromValue(int value)
        {
            return List().Single(e => e.Value == value);
        }
    }
}
