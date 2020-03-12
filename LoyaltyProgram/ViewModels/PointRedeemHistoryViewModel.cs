using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoyaltyProgram.ViewModels
{
    public class PointRedeemHistoryViewModel
    {
        public int RedeemHistoryId { get; set; }

        public double PointsRedeemed { get; set; }
        public DateTime? PointsRedeemedOn { get; set; }
        public String Notes { get; set; }
        public int PromotionId { get; set; }
        public PromotionViewModel Promotion { get; set; }
        public int CustomerId { get; set; }
        public CustomerViewModel Customer { get; set; }
    }
}