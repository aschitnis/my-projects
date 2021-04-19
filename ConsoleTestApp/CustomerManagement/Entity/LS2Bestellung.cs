using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Entity
{
    [Table("LS2Bestellungen")]
    public class LS2Bestellung
    {
        [System.ComponentModel.DataAnnotations.Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string BranchOfficeCode { get; set; }

        [Required]
        public int OrderStatus { get; set; }
        public string OrderNumber { get; set; }
    }
}
