using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace WpfNestedGridApp
{
    /**
     * Application domain specific data from progconfig.xml file 
     * **/
    public class SingletonProgramConfiguration
    {
        private Dictionary<string, string> diProgramConfigurationData = new Dictionary<string, string>();

        private SingletonProgramConfiguration()
        {
            XmlDocument doc = new XmlDocument();
            DirectoryInfo dInfo = new DirectoryInfo(System.IO.Path.Combine(Environment.CurrentDirectory, "config"));
            if(File.Exists(dInfo.FullName+@"\progconfig.xml"))
            {
                doc.Load(dInfo.FullName + @"\progconfig.xml");
               // doc.Load(@"C:\Users\Mustermann\source\repos\ConsoleTestApp\WpfNestedGridApp\BeispielDaten\progconfig.xml");
            }
            else
            {
                doc = null;
            }

            if (doc != null)
            {
                diProgramConfigurationData.Add("startupxaml", doc.DocumentElement.SelectSingleNode("/CONFIG/startupxaml").InnerText);
                diProgramConfigurationData.Add("jsonfile", doc.DocumentElement.SelectSingleNode("/CONFIG/jsonfile").InnerText);
                diProgramConfigurationData.Add("hubstatfolder", doc.DocumentElement.SelectSingleNode("/CONFIG/hubstatfolder").InnerText);
                diProgramConfigurationData.Add("hubstatxmlconfig", doc.DocumentElement.SelectSingleNode("/CONFIG/hubstatxmlconfig").InnerText);
                diProgramConfigurationData.Add("bundxmldata", doc.DocumentElement.SelectSingleNode("/CONFIG/bundxmldata").InnerText);
            }
        }

        private static SingletonProgramConfiguration instance = new SingletonProgramConfiguration();
        public static SingletonProgramConfiguration Instance => instance;

        public Dictionary<string, string> GetConfigurationData()
        {
            return diProgramConfigurationData;
        }
    }
}
