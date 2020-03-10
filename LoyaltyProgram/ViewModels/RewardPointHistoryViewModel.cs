using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoyaltyProgram.ViewModels
{
    public class RewardPointHistoryViewModel
    {
        public int PointHistoryId { get; set; }
        public double PointsEarned { get; set; }
        public String PointsEarnedSource { get; set; }
        public DateTime? PointsEarnedOn { get; set; }
        public DateTime? PointsExpireOn { get; set; }
        public int CustomerId { get; set; }
        //public Customer Customer { get; set; }
    }
}