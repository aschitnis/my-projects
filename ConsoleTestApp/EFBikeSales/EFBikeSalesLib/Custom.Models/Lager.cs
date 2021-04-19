using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFBikeSalesLib.Custom.Models
{
    public class Lager
    {
        private HashSet<Bestellung> _orders;
        private HashSet<Mitarbeitern> _staffs;
        public int StoreId { get; private set; }
        public string StoreName { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string ZipCode { get; private set; }
        public int StaffId { get; private set; }
        public Lager()
        {
            _orders = new HashSet<Bestellung>();
            _staffs = new HashSet<Mitarbeitern>();
        }

        public IEnumerable<Mitarbeitern> Staffs => _staffs?.ToList();
        public IEnumerable<Bestellung> Orders => _orders?.ToList();
    }
}
