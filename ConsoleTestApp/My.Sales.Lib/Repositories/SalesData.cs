using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace My.Sales.Repositories
{
    public class SalesData 
    {
        //Region,Country,Item Type, Sales Channel,Order Priority, Order Date,Order ID, 
        // Ship Date,Units Sold, Unit Price,Unit Cost, Total Revenue,Total Cost, Total Profit
        #region Properties
        public string Region { get; set; }
        public string Country { get; set; }
        public string ItemType { get; set; }
        public string SalesChannel { get; set; }
        public string OrderPriority { get; set; }
        public DateTime OrderDate { get; set; }
        public long OrderId { get; set; }
        public DateTime ShipDate { get; set; }
        #endregion

        #region constructor
        public SalesData()   {  }
        #endregion
    }
}
