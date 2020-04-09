using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LoyaltyProgram.Models
{
    [Table("CustomerLevel")]
    public class CustomerLevel
    {
        [Key]
        public int LevelId { get; set; }
        [StringLength(25)]
        public String LevelName { get; set; }
        public double? PointsRangeFrom { get; set; }
        public double? PointsRangeTo { get; set; }
        [DefaultValue("true")]

        public bool IsActive { get; set; }
        public ICollection<Customer> Customers { get; set; }


    }
}