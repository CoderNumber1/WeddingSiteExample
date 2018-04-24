using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WeddingSite.Models
{
    [Table("GuestCode")]
    public partial class GuestCode
    {
        public int GuestCodeId { get; set; }

        public int? GuestId { get; set; }

        [Column("GuestCode")]
        [Required]
        [StringLength(50)]
        public string GuestCode1 { get; set; }

        public int UseLimit { get; set; }

        public virtual Guest Guest { get; set; }
    }
}