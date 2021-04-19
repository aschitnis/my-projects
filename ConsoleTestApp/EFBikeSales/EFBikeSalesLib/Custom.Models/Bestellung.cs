using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFBikeSalesLib.Custom.Models
{
    public class Bestellung
    {
        private HashSet<AuftragsPositionen> _orderitems;
        public int Id { get; private set; }
        public byte OrderStatus { get; private set; }
        public System.DateTime OrderDate { get; private set; }
        public System.DateTime RequiredDate { get; private set; }
        public Nullable<System.DateTime> ShippedDate { get; private set; }
        public int StoreId { get; private set; }
        public int StaffId { get; private set; }

        // relationships (a Bestellung must have 1 or many Auftragspositionen objects)
        public IEnumerable<AuftragsPositionen> OrderItems => _orderitems?.ToList();
        // a Bestellung has must always have a CustomerID.
        // on the other hand a Customer can (must not) have 1 or many Bestellungen.
        public Nullable<int> CustomerId { get; private set; }
        public Bestellung()
        {
            _orderitems = new HashSet<AuftragsPositionen>();
        }
    }
}
