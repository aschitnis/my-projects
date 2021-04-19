using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Sales.Repositories;

namespace My.Sales.Interfaces
{
    public interface ISalesRepository
    {
        IEnumerable<SalesData> GetAllSales();
    }
}
