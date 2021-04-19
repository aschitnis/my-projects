using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFBikeSalesLib;
using System.Threading.Tasks;

namespace EFBikeSalesLib.Custom.Models
{
    public class Kunde
    {
        private HashSet<Bestellung> _orders;
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string ZipCode { get; private set; }

        public Kunde()
        {
            _orders = new HashSet<Bestellung>();
        }
        // relationships (a Kunde can have 0 to many Orders)
        public IEnumerable<Bestellung> Orders => _orders?.ToList();
    }
}
