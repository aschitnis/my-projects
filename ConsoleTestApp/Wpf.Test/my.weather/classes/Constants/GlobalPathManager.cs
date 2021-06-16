using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Test.my.weather.classes.Constants;

namespace Wpf.Test.my.weather.classes.constants
{
    internal abstract class GlobalPathManager
    {
        private static readonly string DATA_DIRECTORY_NAME = "data";
        private static readonly string CONFIG_DIRECTORY_NAME = "config";
        private static readonly string FILE_EXTENSION_JSON = ".json";

        public static readonly string DIR_CONFIGURATION = System.Windows.Forms.Application.StartupPath + @"\" + CONFIG_DIRECTORY_NAME + @"\";
        public static readonly string DIR_DATA = System.Windows.Forms.Application.StartupPath + @"\" + DATA_DIRECTORY_NAME + @"\";

        public static readonly string FILE_Config_Plaintext = DIR_CONFIGURATION + "schedulerconfig" + FILE_EXTENSION_JSON;
        public static readonly string FILE_Countries_Data_Plaintext = DIR_DATA + "countries" + FILE_EXTENSION_JSON;
        public static readonly string FILE_Currencies_Data_Plaintext = DIR_DATA + "currencies" + FILE_EXTENSION_JSON;

        private static readonly string WEATHER_HTTP_PATH =@"http://api.openweathermap.org/data/2.5/weather?q=";
        private static readonly string WEATHER_WEBSERVICE_LICENSEKEY = @"&units=metric&lang=de&appid=98ec98ab7c0f616ddae7a6c4be445e58";

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
        public static Exception ReadFile(JsonConstants.JsonTypes jsontype, out string filedata)
        {
            Exception exc = null; 
            if (jsontype == JsonConstants.JsonTypes.ScheduleTaskConfiguration)
            {
                if (!File.Exists(FILE_Config_Plaintext))
                {
                    filedata = null;
                    exc = new Exception($"Fehlende Datei: {FILE_Config_Plaintext}");
                }
                else
                {
                    filedata = File.ReadAllText(FILE_Config_Plaintext);
                    exc = ValidateJsonString(filedata);
                }
            }
            else if (jsontype == JsonConstants.JsonTypes.Countries)
            {
                if (!File.Exists(FILE_Countries_Data_Plaintext))
                {
                    filedata = null;
                    exc = new Exception($"Fehlende Datei: {FILE_Countries_Data_Plaintext}");
                }
                else
                {
                    filedata = File.ReadAllText(FILE_Countries_Data_Plaintext);
                    exc = ValidateJsonString(filedata);
                }
            }
            else
            {
                filedata = null;
            }
            return exc;
        }

        public static Exception WriteFile(string json)
        {
            if (File.Exists(FILE_Config_Plaintext))
                File.Delete(FILE_Config_Plaintext);
            File.WriteAllText(FILE_Config_Plaintext, json);
            return null;
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
