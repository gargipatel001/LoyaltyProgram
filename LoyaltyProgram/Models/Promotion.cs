using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LoyaltyProgram.Models
{
    [Table("Promotion")]
    public class Promotion
    {
        [Key]
        public int PromotionId { get; set; }
        [StringLength(25)]
        public String PromotionName { get; set; }
        [StringLength(25)]
        public String PromotionTitle { get; set; }
        
        public String PromotionDesc { get; set; }
        public DateTime? PromotionStartDate { get; set; }
        public DateTime? PromotionEndDate { get; set; }
        public int PartnerId { get; set; }
        public Partner Partner { get; set; }
        public int PromotionTypeId { get; set; }
        public PromotionType PromotionType { get; set; }

    }
}