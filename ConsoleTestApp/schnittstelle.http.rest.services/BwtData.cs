using Newtonsoft.Json;
using schnittstelle.http.rest.services.utility;

namespace schnittstelle.http.rest.services.ssl.certificate
{
    #region Interfaces
    public interface IBwtJson { }
    #endregion

    public static class JsonSerializationGeneric<T> where T : IBwtJson
    {
        public static IBwtJson Deserialize(string jsonString)
        {
            IBwtJson objBwt = JsonConvert.DeserializeObject<IBwtJson>(jsonString);
            return objBwt;
        }
    }

    #region BwtJsonResponseCertificate Class inherits from IBwtCertificate
    public class BwtJsonResponseCertificate
    {
        public string[] wrongAnswers { get; set; }
        public string secret { get; set; }
    }
    #endregion

    #region BwtJsonCertificate Class inherits from IBwtCertificate
    public class BwtJsonCertificate : IBwtJson
    {
        public string Organization { get; set; }
        public string SerialNumber { get; set; }
        public string[] KeyUsageOids { get; set; }
        public int Exponent { get; set; }
        public string Modulus { get; set; }
    }
    #endregion

    /// <summary>
    /// Bereitet die (JSON)Daten vor, die über HTTP-POST versendet werden.
    /// Die Klasse kann zusätz. die Jsondaten serialisieren/deserialisieren. 
    /// </summary>
    public class BwtJsonPostData
    {
        public CommonUtility BwtUtility { get; set; } = new CommonUtility();
        public IBwtJson BwtCertificate { get; set; }

        /// <summary>
        /// Key defined in App.config for Json full filename.
        /// </summary>
        private string JsonFilePathKey { get; } = "certificatejsonfile";
        public string JsonString { get; set; } = null;
    
        public BwtJsonPostData()
        {
            Init();
        }

        private void Init()
        {
            BwtUtility.GetJsonFromTxtFile(JsonFilePathKey);
        }

        private void SerializeJsonCertificateDataToString()
        {

        }

        private void DeserializeJsonCertificateDataToObject()
        {
            if (!string.IsNullOrEmpty(JsonString))
            {
                BwtCertificate = JsonSerializationGeneric<IBwtJson>.Deserialize(JsonString);
            }
        }




        
    }
}
