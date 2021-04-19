using schnittstelle.http.rest.services.utility;
using System.IO;
using System.Text;

namespace ConsoleConnectionTest.text.json
{
    public class BwtJsonFileData : JsonFileData
    {
        #region Constructors
        public BwtJsonFileData() { }
        public BwtJsonFileData(string jsonfilepath)
        {
            JsonFilePath = jsonfilepath;
        }
        #endregion

        public override string JsonFilePath { get; set; }

        /// <summary>
        /// The Certificate data is stored as Json formatted-text and written to a text file.  
        /// </summary>
        public override void WriteJsonToTextFile()
        {
            string jsonText = "{\n\"Organization\":" + "\"" + SSLCertificateUtility.OrganizationString + "\",\n"
                      + "\"SerialNumber\":" + "\"" + SSLCertificateUtility.SerialNumberString + "\",\n"
                      + "\"KeyUsageOids\":[" + "\"" + SSLCertificateUtility.ExtendedKeyUsageOidList[0] + "\",\""
                      + SSLCertificateUtility.ExtendedKeyUsageOidList[1] + "\"],\n"
                      + "\"Exponent\": " + SSLCertificateUtility.ExponentStringDecimalFormat + ",\n"
                      + "\"Modulus\": " + "\"" + SSLCertificateUtility.ModulusStringDecimalFormat + "\"\n } ";

            if (File.Exists(JsonFilePath) == false)
            {
                File.Delete(JsonFilePath);
            }
            File.Create(JsonFilePath).Close();
            File.AppendAllText(JsonFilePath, jsonText, Encoding.UTF8);
        }
    }
}
