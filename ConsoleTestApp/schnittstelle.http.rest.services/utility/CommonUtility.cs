using System;
using System.IO;
using System.Text.RegularExpressions;
using schnittstelle.http.rest.services.pattern;

namespace schnittstelle.http.rest.services.utility
{
    #region Common Functions 
    public class CommonUtility
    {
       // public static Dictionary<string, string> ConfigurationKeyValueDictionary = new Dictionary<string, string>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="replacestring"></param>
        /// <returns></returns>
        public static string InsertColonIfValid(string text, string replacestring)
        {
            if (text.Length % 2 == 0)
            {
                string s = Regex.Replace(text, ".{2}", "$0"+replacestring);
                return s.Remove(s.Length - 1, 1);
            }
            return null;
        }

        /// <summary>
        /// get all the Key-Value pairs in the App.config
        /// </summary>
        /// <param name="config"></param>
        //public void GetAppConfigKeyValueCollection()
        //{
        //    ConfigurationKeyValueDictionary.Clear();
        //    Configuration config = GetConfigurationSettings();
        //    KeyValueConfigurationCollection keyvaluesList = config.AppSettings.Settings;
            
        //    foreach ( KeyValueConfigurationElement e in keyvaluesList )
        //    {
        //        ConfigurationKeyValueDictionary.Add(e.Key, e.Value);
        //    }
        //}

        ///// <summary>
        ///// The app.Config in this Assembly searched for the given key & the value is read out. 
        ///// </summary>
        ///// <param name="config">the App.config for this assembly</param>
        ///// <param name="key">the key contained in the App.config</param>
        ///// <returns></returns>
        //private string GetAppSettingValue(Configuration config, string key)
        //{
        //    KeyValueConfigurationElement element = config.AppSettings.Settings[key];
        //    if (element != null)
        //    {
        //        string value = element.Value;
        //        if (!string.IsNullOrEmpty(value))
        //            return value;
        //    }
        //    return string.Empty;
        //}

        //public string GetValueFromConfigurationKey(string key)
        //{
        //    Configuration config = GetConfigurationSettings();
        //    if (config != null)
        //    {
        //        return GetAppSettingValue(config, key);
        //    }
        //    else
        //    {
        //        // Fehler Nachricht einbauen !!!
        //    }
        //    return null;
        //}

        /// <summary>
        /// insert a seperator-value in a string repeatedly. 
        /// E.g. insert colon(:) in 2345674319 after every 2 characters -> 23:45:67:43:19
        /// The String has to have a even number length
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string AddSeparatorInString(int index,string replacement,string value)
        {
            if (value.Length % 2 == 0)
            {
                string pattern = ".{" + index.ToString() + "}";
                string replacementpattern = "$0" + replacement;
                string stringWithColon = Regex.Replace(value, pattern, replacementpattern);
                return stringWithColon.Remove(stringWithColon.Length - 1, 1);
            }
            return null;
        }

        /// <summary>
        /// Die Name (mit Pfad) der Jsondatei wird aus dem App.config ermittelt.
        /// Die Inhalt der Jsondatei wurde ausgelesen.
        /// </summary>
        /// <param name="key">Das Key-Value KEY unter App.config Datei</param>
        /// <returns>Den Inhalt der Jsondatei als String</returns>
        public string GetJsonFromTxtFile(string key)
        {
            string jsonFileName = null;
            string jsonString = null;

            jsonFileName = SingletonConfigurationData.Instance.GetAppSettingValue(key);

            if (File.Exists(jsonFileName))
            {
                jsonString = File.ReadAllText(jsonFileName);
                return jsonString;
            }
            else
            {
                Console.WriteLine(".....Datei mit Json nicht vorhanden");
                return jsonString;
            }
        }

        ///// <summary>
        ///// Die App.config für diesen Assembly wird ermittelt  
        ///// </summary>
        ///// <returns>
        ///// Die Key-Value Inhalte der App.config für diesen Assembly werden zurückgeliefert. 
        ///// </returns>
        //private Configuration GetConfigurationSettings()
        //{
        //    Configuration config = null;
        //    string exeConfigPath = this.GetType().Assembly.Location;
        //    try
        //    {
        //        config = ConfigurationManager.OpenExeConfiguration(exeConfigPath);
        //    }
        //    catch (Exception e)
        //    {
        //        config = null;
        //        /** log to file **/
        //        Console.WriteLine("Fehler: {0}", e.Message);
        //    }
        //    return config;
        //}
    }
    #endregion


}
