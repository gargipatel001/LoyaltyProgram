using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoyaltyProgram.ViewModels
{
    public class PartnerViewModel
    {
        public int PartnerId { get; set; }
        public String PartnerName { get; set; }
        public String PartnerLogo { get; set; }
        public String PartnerDesc { get; set; }
        public String PartnerWebLink { get; set; }
        ICollection<PromotionViewModel> Promotions { get; set; }

    }
}