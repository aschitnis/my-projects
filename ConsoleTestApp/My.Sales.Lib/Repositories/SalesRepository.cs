using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Sales.Models;
using My.Sales.Interfaces;
using My.Sales.Classes;

namespace My.Sales.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        public IEnumerable<SalesData> GetAllSales()
        {
            string salesCsvDataString = null;
            
           // Exception ex = BaseDataSource.ReadSalesFile(out salesCsvDataString);
            if (ex == null)
            {
                return new List<SalesData>();
            }
            return new List<SalesData>();
        }

    }
}
