using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoyaltyProgram.ViewModels
{
    public class ContactUsViewModel
    {
        public String CustomerEmail { get; set; }
        public String CustomerFirstName { get; set; }

        public String CustomerLastName { get; set; }
        public String CustomerPhoneNumber { get; set; }
        public String CustomerCardNo { get; set; }

        public String Comment { get; set; }
    }
}