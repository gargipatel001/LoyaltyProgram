using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LoyaltyProgram.Models
{
    [Table("Partner")]
    public class Partner
    {
        [Key]
        public int PartnerId { get; set; }
        [StringLength(25)]
        public String PartnerName { get; set; }
        public String PartnerLogo { get; set; }
        public String PartnerDesc { get; set; }
        public String PartnerWebLink { get; set; }
        [DefaultValue("true")]

        public bool IsActive { get; set; }
        ICollection<Promotion> Promotions { get; set; }

    }
}