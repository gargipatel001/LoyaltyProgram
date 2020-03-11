using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoyaltyProgram.ViewModels
{
    public class PromotionViewModel
    {
        public int PromotionId { get; set; }
     
        public String PromotionName { get; set; }
      
        public String PromotionTitle { get; set; }

        public String PromotionDesc { get; set; }
        public DateTime? PromotionStartDate { get; set; }
        public DateTime? PromotionEndDate { get; set; }
        public int PartnerId { get; set; }
        public int PromotionPoints { get; set; }
        public PartnerViewModel Partner { get; set; }
        public int PromotionTypeId { get; set; }
        public PromotionViewModel PromotionType { get; set; }
    }
}