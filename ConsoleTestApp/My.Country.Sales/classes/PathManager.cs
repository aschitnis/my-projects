using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my.country.sales.classes
{
    internal sealed class PathManager
    {
        public bool AreFilesAvailable { get; private set; } = false;
        public string CsvFilePathSales { get; private set; }
        public string JsonFilePathCountries { get; private set; }
        public string JsonFilePathCurrencies { get; private set; }
        public PathManager()
        {
            Init();
        }

        private void Init()
        {
            string execFolderfullpath = this.GetType().Assembly.Location.Substring(0, this.GetType().Assembly.Location.LastIndexOf(@"\"));
            CsvFilePathSales = execFolderfullpath + @"\data\500000_Sales_Records.csv";
            JsonFilePathCountries = execFolderfullpath + @"\data\countries.json";
            JsonFilePathCurrencies = execFolderfullpath + @"\data\currencies.json";
        }
        public Exception CheckIfFilesExist()
        {
            AreFilesAvailable = false;
            if (File.Exists(CsvFilePathSales))
            {
                if (File.Exists(JsonFilePathCountries))
                {
                    if (File.Exists(JsonFilePathCurrencies))
                    {
                        AreFilesAvailable = true;
                        return null;
                    }
                    else
                    {
                        return new Exception("currencies.json not found.");
                    }
                }
                else
                {
                    return new Exception("countries.json not found.");
                }
            }
            else
            {
                return new Exception("500000_Sales_Records.csv not found.");
            }
        }
    }
}
