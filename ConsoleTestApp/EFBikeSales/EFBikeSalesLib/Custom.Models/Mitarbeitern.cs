using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFBikeSalesLib.Custom.Models
{
    public class Mitarbeitern
    {
        private HashSet<Bestellung> _orders;
        public int StaffId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public byte Active { get; private set; }
        public int StoreId { get; private set; }
        public Nullable<int> ManagerId { get; private set; }

        public Mitarbeitern()
        {
            _orders = new HashSet<Bestellung>();
        }
        // relationships (a Mitarbeiter can have 0 to many Orders)
        public IEnumerable<Bestellung> Orders => _orders?.ToList();
    }
}
