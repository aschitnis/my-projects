using schnittstelle.http.rest.services.utility;
using System.IO;
using System.Text;

namespace ConsoleConnectionTest.text.json
{
    public class UdemyJsonFileData : JsonFileData
    {
        public override string JsonFilePath { get; set; }

        #region Constructors
        public UdemyJsonFileData() { }
        public UdemyJsonFileData(string jsonfilepath)
        {
            JsonFilePath = jsonfilepath;
        }
        #endregion

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
