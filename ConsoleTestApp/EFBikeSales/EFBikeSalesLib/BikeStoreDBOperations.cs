using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFBikeSalesLib
{
    public class BikeStoreDBOperations
    {
        public static DbSet<customer> GetAllCustomers()
        {
            var context = new BikeStoresEntities();
            try
            {
                return context.customers;
            }
            catch (InvalidOperationException e)
            {
                throw e;
            }
        }
        public static customer GetCustomerById(int customerId)
        {
            var context = new BikeStoresEntities();
            try
            {
                customer searchedCustomer = context.customers.Find(1445);
                return searchedCustomer;
            }
            catch(InvalidOperationException e)
            {
                throw e;
            }
        }

        public Exception SaveNewCustomer(customer _customer)
        {
            Exception ex = null;
            try
            {
                var context = new BikeStoresEntities();
                context.customers.Add(_customer);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                ex = e;
            }
            return ex;
        }
    }
}
