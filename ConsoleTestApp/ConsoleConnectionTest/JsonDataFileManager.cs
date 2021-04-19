using ConsoleConnectionTest.text.json;
using schnittstelle.http.rest.services.utility;
using schnittstelle.http.rest.services.certificate.properties;

namespace ConsoleConnectionTest.root
{
    public class JsonDataFileManager
    {
        public SSLCertificateUtility SSLCertificateData { get; set; } = null;

        #region constructor
        public JsonDataFileManager(CertificateType.CertificateOwner enumcompanyname)
        {
            ReadCertificateAbsoluteFileNameFromAppConfig(enumcompanyname);
        }

        public JsonDataFileManager()
        {

        }
        #endregion

        /// <summary>
        /// gets the .crt file path for the companyname (enum) from the App.config file.
        /// </summary>
        /// <param name="enumcompanyname">the enum options are specified in the CertificateOwner enum type.
        ///                               The name of the Certificate Owner.
        /// </param>
        private void ReadCertificateAbsoluteFileNameFromAppConfig(CertificateType.CertificateOwner enumcompanyname)
        {
            SSLCertificateData = new SSLCertificateUtility(enumcompanyname);
        }


        public void GetDataFromSSLCertificate()
        {
            SSLCertificateData.ReadPropertiesFromCertificate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonformat"></param>
        /// <param name="destinationfilename">the new file to write the json-data into.</param>
        public void WriteToFile(JsonFileData jsonformat, string destinationfilename)
        {
            if (SSLCertificateUtility.IsCertificateReadSuccess)
            {
                jsonformat.JsonFilePath = destinationfilename;
                jsonformat.WriteJsonToTextFile();
            }
        }
    }
}
