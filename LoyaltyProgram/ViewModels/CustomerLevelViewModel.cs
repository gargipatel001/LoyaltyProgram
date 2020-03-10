using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoyaltyProgram.ViewModels
{
    public class CustomerLevelViewModel
    {
        public int LevelId { get; set; }
        
        public String LevelName { get; set; }
        public double PointsRangeFrom { get; set; }
        public double PointsRangeTo { get; set; }
       
        //public ICollection<Customer> Customers { get; set; }
    }
}