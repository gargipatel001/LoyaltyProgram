using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LoyaltyProgram.Models
{
    [Table("PointRedeemHistory")]
    public class PointRedeemHistory
    {
        [Key]
        public int RedeemHistoryId { get; set; }

        public double PointsRedeemed { get; set; }
        public DateTime? PointsRedeemedOn { get; set; }
        public String Notes { get; set; }
        public int PromotionId { get; set; }
        public Promotion Promotion { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}