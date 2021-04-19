using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp.temp.classes
{
    #region abstract class
    public abstract class BaseCharacterSetEncoding
    {
        public abstract string FileName { get; }
        public List<StructDecimalAndBinaryRepresentation> DecimalAndBinaryValuesList { get; set; }
        public struct StructDecimalAndBinaryRepresentation
        {
            public StructDecimalAndBinaryRepresentation(string decimalFormat, string binaryFormat)
            {
                DecimalFormat = decimalFormat ?? default(string);
                BinaryFormat = binaryFormat ?? default(string);
                FullBinaryFormat = default(string);
            }

            public string DecimalFormat { get; }
            public string BinaryFormat { get; }
            public string FullBinaryFormat { get; set; }

            public override string ToString() => $"{DecimalFormat}  - {BinaryFormat}";
        }
        public abstract void WriteToFile(string text);
        public abstract string ReadFromFile();
        public abstract void GetDecimalAndBinaryValuesOfEncodedText();
    }
    #endregion

    #region UTF8 Unicode Encoding Class
    public class UnicodeCharacterSetEncoding : BaseCharacterSetEncoding
    {
        public override string FileName { get { return @"E:\python\batchscripts\unicode.txt"; } }
        public override void WriteToFile(string text)
        {
            if (File.Exists(this.FileName))
                File.Delete(this.FileName);

            Stream fileStream = new FileStream(this.FileName,FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fileStream, System.Text.Encoding.Unicode))
            {
                writer.WriteLine(text);
                writer.Flush();
            }
        }
        public override string ReadFromFile()
        {
            string text = null;
            Stream fileStream = new FileStream(this.FileName, FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream, System.Text.Encoding.Unicode))
            {
                text = reader.ReadLine();
            }
            return text;
        }
        public override void GetDecimalAndBinaryValuesOfEncodedText()
        {
            string text = null;
            byte[] bytes;

            text = ReadFromFile();
            bytes = Encoding.Unicode.GetBytes(text);

            // initialize the struct
            DecimalAndBinaryValuesList = new List<StructDecimalAndBinaryRepresentation>();

            // loop through each Byte & convert it into its decimal and its binary representational value.
            // store the value in the Struct List.
            for (int i = 0; i <= bytes.Length - 1; i++)
            {
                string binaryString = Convert.ToString(bytes[i]); // z.b. 10100110
                int decimalValue = Convert.ToInt32(binaryString); // z.b. 166
                StructDecimalAndBinaryRepresentation structencodingValues = new StructDecimalAndBinaryRepresentation(binaryString, Convert.ToString(decimalValue, 2));
                
                DecimalAndBinaryValuesList.Add(structencodingValues);
            }
        }
    }
    #endregion

    #region UTF8 Encoding Class
    public class UTF8CharacterSetEncoding : BaseCharacterSetEncoding
    {
        public override string FileName { get { return @"E:\python\batchscripts\utf8.txt"; } }
        public override void WriteToFile(string text)
        {
            if (File.Exists(this.FileName))
                File.Delete(this.FileName);

            Stream fileStream = new FileStream(this.FileName, FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fileStream, System.Text.Encoding.UTF8))
            {
                writer.WriteLine(text);
                writer.Flush();
            }
        }
        public override string ReadFromFile()
        {
            string text = null;
            Stream fileStream = new FileStream(this.FileName, FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream, System.Text.Encoding.UTF8))
            {
                text = reader.ReadLine();
            }
            return text;
        }
        public override void GetDecimalAndBinaryValuesOfEncodedText()
        {
            string text = null;
            byte[] bytes;

            text = ReadFromFile();
            bytes = Encoding.UTF8.GetBytes(text);

            // initialize the struct
            DecimalAndBinaryValuesList = new List<StructDecimalAndBinaryRepresentation>();

            // loop through each Byte & convert it into its decimal and its binary representational value.
            // store the value in the Struct List.
            for (int i = 0; i <= bytes.Length - 1; i++)
            {
                string binaryString = Convert.ToString(bytes[i]); // z.b. 10100110
                int decimalValue = Convert.ToInt32(binaryString); // z.b. 166
                StructDecimalAndBinaryRepresentation structencodingValues = new StructDecimalAndBinaryRepresentation(binaryString, Convert.ToString(decimalValue, 2));

                DecimalAndBinaryValuesList.Add(structencodingValues);
            }
        }
    }
    #endregion
}
