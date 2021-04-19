using NLog;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml;

namespace schnittstelle.mysql.library.singleton.configurations
{
    /// <summary>
    /// singleton pattern angewendet
    /// </summary>
    public class ConfigurationData
    {
        private static readonly NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();

        private string DbConnectionString { get; set; }
        public Dictionary<string,List<string>> TablesSchemaDictionary { get; set; }
        private Configuration config { get; set; } 

        private ConfigurationData()
        {
            TablesSchemaDictionary = new Dictionary<string, List<string>>();
            config = GetConfigurationSettings();

            Init();
        }

        private void Init()
        {
            DbConnectionString      = config.AppSettings.Settings["mysqlconnectionString"].Value;
            string logfile          = config.AppSettings.Settings["logdirpath"].Value + @"\log_" + DateTime.Now.ToString() + ".txt";
            string tablesschemafile = config.AppSettings.Settings["tableschemaxml"].Value;

            SetLoggingConfiguration(logfile);

            ReadTablesSchemaFromXml(tablesschemafile);
        }

        private void ReadTablesSchemaFromXml(string xmlfile)
        {
            XmlDocument xtableschema = new XmlDocument();
            if (File.Exists(xmlfile))
            {
                xtableschema.Load(xmlfile);

                List<string> booksschemalist = new List<string>();
                booksschemalist.Add(xtableschema.DocumentElement.SelectSingleNode("/booksorders/books/book_id").InnerText);
                booksschemalist.Add(xtableschema.DocumentElement.SelectSingleNode("/booksorders/books/title").InnerText);
                booksschemalist.Add(xtableschema.DocumentElement.SelectSingleNode("/booksorders/books/description").InnerText);
                booksschemalist.Add(xtableschema.DocumentElement.SelectSingleNode("/booksorders/books/author").InnerText);
                TablesSchemaDictionary.Add("books", booksschemalist);

                List<string> currenciesschemalist = new List<string>();
                currenciesschemalist.Add(xtableschema.DocumentElement.SelectSingleNode("/booksorders/currencies/currency_id").InnerText);
                currenciesschemalist.Add(xtableschema.DocumentElement.SelectSingleNode("/booksorders/currencies/name").InnerText);
                currenciesschemalist.Add(xtableschema.DocumentElement.SelectSingleNode("/booksorders/currencies/tag").InnerText);
                TablesSchemaDictionary.Add("currencies", currenciesschemalist);

                List<string> sellersschemalist = new List<string>();
                sellersschemalist.Add(xtableschema.DocumentElement.SelectSingleNode("/booksorders/sellers/seller_id").InnerText);
                sellersschemalist.Add(xtableschema.DocumentElement.SelectSingleNode("/booksorders/sellers/name").InnerText);
                TablesSchemaDictionary.Add("sellers", sellersschemalist);

                List<string> orderstatusschemalist = new List<string>();
                orderstatusschemalist.Add(xtableschema.DocumentElement.SelectSingleNode("/booksorders/orderstatus/orderstatus_id").InnerText);
                orderstatusschemalist.Add(xtableschema.DocumentElement.SelectSingleNode("/booksorders/orderstatus/statusname").InnerText);
                TablesSchemaDictionary.Add("orderstatus", orderstatusschemalist);

                _log.Info(string.Format("- Tabellen_Schema aus {0} erfolgreich geladen - {1}", "tableschema.xml", DateTime.Now.ToString()));
            }
            else
            {
                _log.Fatal(string.Format("- Tabellen_Schemadatei nicht gefunden - {0}", DateTime.Now.ToString()));
            }
        }

        private void SetLoggingConfiguration(string _logfile)
        {
            NLog.Config.LoggingConfiguration logconfig = new NLog.Config.LoggingConfiguration();
            FileTarget logfile = new NLog.Targets.FileTarget("logfile") { FileName = _logfile };
            logconfig.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);

            // Apply logging config
            NLog.LogManager.Configuration = logconfig;
        }

        private static ConfigurationData instance = new ConfigurationData();
        public static ConfigurationData Instance => instance;

        public string GetMySqlConnectionString()
        {
            return DbConnectionString;
        }

        public Dictionary<string, List<string>> GetTablesSchema()
        {
            return TablesSchemaDictionary;
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
                // Console.WriteLine("Fehler: {0}", e.Message);
            }
            return config;
        }
    }
}
