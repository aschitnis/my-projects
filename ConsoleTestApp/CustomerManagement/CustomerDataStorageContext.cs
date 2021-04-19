using CustomerManagement.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement
{
    public class CustomerDataStorageContext : DbContext
    {
        public CustomerDataStorageContext() : base("name=LS2.0CustomerDataStorageContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CustomerDataStorageContext, CustomerManagement.Migrations.Configuration>());
        }

        public DbSet<LS2Bestellung> LS2Bestellungen { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
