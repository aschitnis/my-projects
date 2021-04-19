using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Test.my.books.management.Classes
{
    internal abstract class PathManager
    {
        private static readonly string SAVE_DIRECTORY_NAME = "Save";
        private static readonly string FILE_EXTENSION_JSON = ".json";

        public static readonly string DIR_Save = System.Windows.Forms.Application.StartupPath + @"\" + SAVE_DIRECTORY_NAME + @"\";
        
        public static readonly string FILE_Books_Plaintext = DIR_Save + "books" + FILE_EXTENSION_JSON;
        public static readonly string FILE_Index_Plaintext = DIR_Save + "index" + FILE_EXTENSION_JSON;
    }
}
