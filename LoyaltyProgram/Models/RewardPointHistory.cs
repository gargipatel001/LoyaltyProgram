using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LoyaltyProgram.Models
{
    [Table("RewardPointHistory")]
    public class RewardPointHistory
    {
        [Key]
        public int PointHistoryId { get; set; }
        public double PointsEarned { get; set; }
        public String PointsEarnedSource { get; set; }
        public DateTime? PointsEarnedOn { get; set; }
        public DateTime? PointsExpireOn { get; set; }
        public int CustomerId { get; set; }
        [DefaultValue("true")]
        public bool IsActive { get; set; }
        public Customer Customer { get; set; }
               
    }
}