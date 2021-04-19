using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TestDomainModel
{
    public partial class KundenModel : DbContext
    {
        public KundenModel()
            : base("name=KundenModel")
        {
        }

        public virtual DbSet<tbKunden> tbKunden { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
