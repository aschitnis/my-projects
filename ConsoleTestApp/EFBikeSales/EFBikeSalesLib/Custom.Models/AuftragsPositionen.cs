using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFBikeSalesLib.Custom.Models
{
    public class AuftragsPositionen
    {
        // relationships : OrderId ist a FK related to Bestellungen.OrderId
        public int OrderId { get; private set; }
        public int ItemId { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal ListPrice { get; private set; }
        public decimal Discount { get; private set; }
    }
}
