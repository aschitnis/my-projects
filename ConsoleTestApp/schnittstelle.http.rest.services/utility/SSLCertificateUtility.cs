using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using schnittstelle.http.rest.services.certificate.properties;
using System.Security.Cryptography.X509Certificates;
using schnittstelle.http.rest.services.pattern;

namespace schnittstelle.http.rest.services.utility
{
    /// <summary>
    /// Functions to extract Information from a SSL-Certificate e.g. Public key, modulus etc..
    /// Functions from a external Library (Chilkat) are used extensively. 
    /// Library "Chilkat" was installed over NuGet Package manager. Chilkat Dokumentation : https://www.example-code.com/csharp/cert.asp
    /// Digital Certificate explained: https://knowledge.digicert.com/solution/SO18140.html
    /// Manage Digi-Cert. with .Net Framework: https://www.red-gate.com/simple-talk/dotnet/net-framework/beginning-with-digital-signatures-in-net-framework/
    /// </summary>
    #region SSLCertificate
    public class SSLCertificateUtility
    {
        public enum ModuloFormat
        {
            Decimal,
            Hexadecimal
        }

        public static Chilkat.Cert BwtCertificateObject = new Chilkat.Cert();
        public static Chilkat.Xml XmlPublicKey = new Chilkat.Xml();
        public static List<string> ExtendedKeyUsageOidList = new List<string>();
        public static string CertificateFilePath;
        public static string OrganizationString = null;
        public static string SerialNumberString = null;
        public static string ModulusStringDecimalFormat = null;
        public static string ExponentStringDecimalFormat = null;
        public static string ModulusStringHexadecimalFormat = null;
        public static bool IsCertificateReadSuccess = false;
        public static string[] ArrayCertificateTypes = new string[] { "bwtsslcertificate", "udemysslcertificate" };
        
        public static CommonUtility UtilityObject = new CommonUtility();

        #region Constructors
        public SSLCertificateUtility()
        {
            /**
            CertificateFilePath = UtilityObject.GetValueFromConfigurationKey("sslcertificatefile");
            **/
        }

        public SSLCertificateUtility(CertificateType.CertificateOwner enumcertificateype)
        {
            CertificateFilePath = GetZertifikatDateiName(enumcertificateype);
        }
        #endregion


        private string GetZertifikatDateiName(CertificateType.CertificateOwner enumcertificateype)
        {
            Dictionary<string, string> dctConfigData = SingletonConfigurationData.Instance.GetAppConfigData();

            if (enumcertificateype == CertificateType.CertificateOwner.bwt)
            {
                return dctConfigData[ArrayCertificateTypes[0]];
            }
            else if (enumcertificateype == CertificateType.CertificateOwner.udemy)
            {
                return dctConfigData[ArrayCertificateTypes[1]];
            }
            else return null;
        }

        private bool IsLoadCertificateFromFileSuccessful()
        {
            bool certSuccess = BwtCertificateObject.LoadFromFile(CertificateFilePath);
            if (certSuccess != true)
            {
                Console.WriteLine(BwtCertificateObject.LastErrorText);
                return certSuccess;
            }
            return certSuccess;
        }

        /// <summary>
        /// reads the properties from the *.cert/*.crt file
        /// </summary>
        /// <param name="certificatefilename">absolute name of the certificate file</param>
        public void ReadPropertiesFromCertificate()
        {
            ModuloFormat enumformatTest = ModuloFormat.Decimal;

            if (IsLoadCertificateFromFileSuccessful())
            {
                Chilkat.PublicKey pubKey = BwtCertificateObject.ExportPublicKey();
                /** load public key data in XML Format  **/
                XmlPublicKey.LoadXml(pubKey.GetXml());

                // Serial number
                string serialNumber = BwtCertificateObject.SerialNumber;
                SerialNumberString = CommonUtility.AddSeparatorInString(2, ":", serialNumber);
                // Organization
                OrganizationString = BwtCertificateObject.IssuerO;

                /*** get Extended Key-Usage Oids ***/
                X509Certificate2 ct = new X509Certificate2();
                ct.Import(CertificateFilePath);

                foreach (X509Extension ext in ct.Extensions)
                {
                    if (ext is X509EnhancedKeyUsageExtension)
                    {
                        OidCollection oidList = (ext as X509EnhancedKeyUsageExtension).EnhancedKeyUsages;
                        foreach (Oid oid in oidList)
                        {
                            ExtendedKeyUsageOidList.Add(oid.Value);
                        }
                    }
                }
                /*********   *******************/

                /*** Get Modulus and Exponent *****/
                // get the base64 modulus 
                string sModulus = XmlPublicKey.GetChildContent("Modulus");
                //  get the base64 exponent 
                string sExponent = XmlPublicKey.GetChildContent("Exponent");

                //  convert Modulus to hex or dec:
                Chilkat.BinData binDat = new Chilkat.BinData();
                binDat.AppendEncoded(sModulus, "base64");

                if (enumformatTest == ModuloFormat.Decimal)
                {
                    ModulusStringDecimalFormat = binDat.GetEncoded("dec");
                }
                else
                {
                    ModulusStringHexadecimalFormat = binDat.GetEncoded("hex");
                }

                // Get the exponent in decimal format
                binDat.Clear();
                binDat.AppendEncoded(sExponent, "base64");
                ExponentStringDecimalFormat = binDat.GetEncoded("dec"); ;
                /****     **********/
                IsCertificateReadSuccess = true;
            }
            else { IsCertificateReadSuccess = false; }
        }
    }
    #endregion
}
