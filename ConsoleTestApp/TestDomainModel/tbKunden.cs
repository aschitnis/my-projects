namespace TestDomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbKunden")]
    public partial class tbKunden
    {
        [Key]
        [Column(Order = 0)]
        public Guid id { get; set; }

        [StringLength(50)]
        public string firstname { get; set; }

        [StringLength(50)]
        public string lastname { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string location { get; set; }
    }
}
