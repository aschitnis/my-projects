using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace schnittstelle.http.rest.services.pattern
{
    /// <summary>
    /// reads all the Key-Value pairs from App.config
    /// </summary>
    public class SingletonConfigurationData
    {
        private Dictionary<string, string> appConfigDictionary = new Dictionary<string, string>();

        private SingletonConfigurationData()
        {
            ReadConfigurationKeyValues();
        }

        private static SingletonConfigurationData instance = new SingletonConfigurationData();
        public static SingletonConfigurationData Instance => instance;

        /// <summary>
        /// read all the Key-Value pairs in the App.config file & store them in a Dictionary object.
        /// </summary>
        /// <param name="config"></param>
        private void ReadConfigurationKeyValues()
        {
            appConfigDictionary.Clear();
            Configuration config = GetConfigurationSettings();
            KeyValueConfigurationCollection keyvaluesList = config.AppSettings.Settings;

            foreach (KeyValueConfigurationElement e in keyvaluesList)
            {
                appConfigDictionary.Add(e.Key, e.Value);
            }
        }

        /// <summary>
        /// returns all the Key-Values as a Dictionary<> Object from the App.config file. 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,string> GetAppConfigData()
        {
            return appConfigDictionary;
        }

        /// <summary>
        /// The app.Config in this Assembly searched for the given key & the value is read out. 
        /// </summary>
        /// <param name="key">the key contained in the App.config</param>
        /// <returns></returns>
        public string GetAppSettingValue(string key)
        {
             return appConfigDictionary.Where((k) => k.Key == key).FirstOrDefault().Value;

            //Configuration config = GetConfigurationSettings();

            //KeyValueConfigurationElement element = config.AppSettings.Settings[key];
            //if (element != null)
            //{
            //    string value = element.Value;
            //    if (!string.IsNullOrEmpty(value))
            //        return value;
            //}
            // return string.Empty;
        }

        /// <summary>
        /// Die App.config für diesen Assembly wird ermittelt  
        /// </summary>
        /// <returns>
        /// Die Key-Value Inhalte der App.config für diesen Assembly werden zurückgeliefert. 
        /// </returns>
        private Configuration GetConfigurationSettings()
        {
            Configuration config = null;
            string exeConfigPath = this.GetType().Assembly.Location;
            try
            {
                config = ConfigurationManager.OpenExeConfiguration(exeConfigPath);
            }
            catch (Exception e)
            {
                config = null;
                /** log to file **/
                Console.WriteLine("Fehler: {0}", e.Message);
            }
            return config;
        }
    }
}
