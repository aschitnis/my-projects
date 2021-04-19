using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFBikeSalesLibrary
{
    public class BikeStoreOperation
    {
        public void SaveNewCustomer()
        {
            var context = new BikeStoresEntities();
            customer kunde = new customer() { first_name = "Elfriede", last_name = "Krbecek", city = "Salzburg Stadt", phone = "0043 650 4065003", state = "Salzburg", street = "Erentrudisstrasse 23", zip_code = "5020", email = "khg_elfi@hotmail.com" };
            context.customers.Add(kunde);
            context.SaveChanges();

        }
    }
}
