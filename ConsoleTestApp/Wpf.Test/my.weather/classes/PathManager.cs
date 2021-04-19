using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Test.my.weather.classes
{
    internal abstract class PathManager
    {
        private static readonly string CONFIG_DIRECTORY_NAME = "config";
        private static readonly string FILE_EXTENSION_JSON = ".json";
        public static readonly string DIR_CONFIGURATION = System.Windows.Forms.Application.StartupPath + @"\" + CONFIG_DIRECTORY_NAME + @"\";

        public static readonly string FILE_Config_Plaintext = DIR_CONFIGURATION + "schedulerconfig" + FILE_EXTENSION_JSON;
        
        private static readonly string WEATHER_HTTP_PATH =@"http://api.openweathermap.org/data/2.5/weather?q=";
        private static readonly string WEATHER_WEBSERVICE_LICENSEKEY = @"&lang=de&appid=98ec98ab7c0f616ddae7a6c4be445e58";

        #region methods
        internal static string GetWebServiceUrl(string _city)
        {
            return WEATHER_HTTP_PATH + _city + WEATHER_WEBSERVICE_LICENSEKEY;
        }

        /// <summary>
        /// Read the json file containing the schedule times to start & end a Task.
        /// Following checks are performed: a) File exists or not.
        ///                                 b) validity of json structure
        /// </summary>
        /// <returns></returns>
        internal static Exception ReadFile(out string filedata)
        {
            Exception exc = null; 
            if (!File.Exists(FILE_Config_Plaintext))
            {
                filedata = null;
                exc = new Exception($"Fehlende Datei: {FILE_Config_Plaintext}") ;
            }
            else
            {
                filedata = File.ReadAllText(FILE_Config_Plaintext);
                exc = ValidateJsonString(filedata);
            }
            return exc;
        }

        private static Exception ValidateJsonString(string json)
        {
            Exception exc = null;
            try
            {
                var jsonObject = JObject.Parse(json);
            }
            catch (JsonReaderException ex)
            {
                exc = ex;
            }
            return exc;
        }
        #endregion
    }
}
