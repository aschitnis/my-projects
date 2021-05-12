using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDataEncryption.classes
{
    public abstract class Constants
    {
        public const string PERSONAL_DATA_ENCRYPTION_DEFAULT_PASSWORD = "w34HuZG$ed:Mt4t4";
        public const string PERSONAL_DATA_ENCRYPTION_SALT = "2xKQnPI6U4t3uBRO5oIRJCTFlXtw3G";
        
        //public const string PASSWORD_ENCRYPTION_SALT = "vwAB45FXbmHgWB46mtfVu6GNSamz529M";
        public static readonly byte[] ENCRYPTION_INIT_VECTOR = Encoding.ASCII.GetBytes("MDjjt3JB2C7fzvhX");

        public static readonly Encoding DEFAULT_FILE_ENCODING = Encoding.UTF8;
    }
}
