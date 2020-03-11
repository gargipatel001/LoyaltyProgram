using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoyaltyProgram.ViewModels
{
    public class PromotionTypeViewModel
    {
        public int PromotionTypeId { get; set; }
       
        public String PromotionTypeName { get; set; }
        ICollection<PromotionViewModel> Promotions { get; set; }
    }
}