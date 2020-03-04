using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LoyaltyProgram.Models
{
    [Table("PromotionType")]
    public class PromotionType
    {
        [Key]
        public int PromotionTypeId { get; set; }
        [StringLength(25)]
        public String PromotionTypeName { get; set; }
        ICollection<Promotion> Promotions { get; set; }
    }
}