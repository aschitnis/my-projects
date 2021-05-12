using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDataEncryption.classes
{
    internal abstract class PathManager
    {
        public const string FILE_EXTENSION_ENCRYPTED = ".aes";
        public const string FILE_EXTENSION_XML = ".xml";

        public const string SAVE_DIRECTORY_NAME = "Save";
        public static string DIR_Save = System.Windows.Forms.Application.StartupPath + @"\" + SAVE_DIRECTORY_NAME + @"\";

        public static string FILE_Data_Encrypted = DIR_Save + "what" + FILE_EXTENSION_ENCRYPTED;
        public static string FILE_Data_Plaintext = DIR_Save + "what" + FILE_EXTENSION_XML;
    }
}
