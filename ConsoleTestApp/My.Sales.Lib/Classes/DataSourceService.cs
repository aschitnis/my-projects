using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace My.Sales.Classes
{
    public class DataSourceService
    {
        private BaseConstants baseConstants { get; }
        private static DataSourceService _instance => new DataSourceService();

        #region Constructor
        private DataSourceService()
        {
            baseConstants = new BaseConstants();
        }
        #endregion

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static DataSourceService GetInstance()
        {
            return _instance;
        }

        private Exception ValidateData()
        {
            if (!File.Exists(baseConstants.SalesFileAbsolutePath))
                return new Exception($"File not found - {baseConstants.SalesFileAbsolutePath}");
            return null;
        }
        private Exception ReadSalesFile(out string salescsvdatastring)
        {
            salescsvdatastring = null;
            Exception ex = ValidateData();
            if (ex == null)
            {
                salescsvdatastring = File.ReadAllText(baseConstants.SalesFileAbsolutePath);
            }
            return ex;
        }

        public Exception ReadSalesDataIntoSalesEntityModel(ref )
        {
            string salescsvdatastring;
            Exception ex = ReadSalesFile(out salescsvdatastring);
            if (ex == null)
            {

            }
            return ex;
        }
        internal static string ReadCurrenciesFile()
        {
            throw new NotImplementedException();
        }
        internal static string ReadCountriesFile()
        {
            throw new NotImplementedException();
        }
    }
}
