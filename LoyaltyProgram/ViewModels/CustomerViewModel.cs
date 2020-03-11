using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoyaltyProgram.ViewModels
{
    public class CustomerViewModel
    {
       
        public int CustomerId { get; set; }

        public String CustomerEmail { get; set; }
        public String CustomerPassword { get; set; }
        public String CustomerCPassword { get; set; }

        public String CustomerFirstName { get; set; }

        public String CustomerLastName { get; set; }
        public String CustomerAddress
        { get; set; }
        
        public String CustomerCity
        { get; set; }
        
        public String CustomerProvince { get; set; }
        
        public String CustomerPostalCode
        { get; set; }

        public DateTime? CustomerDOB { get; set; }
        
        public String CustomerPhoneNumber { get; set; }
        
        public String CustomerGender { get; set; }
        
        public String CustomerCardNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Double CustomerLoyaltyPoints { get; set; }
        public bool IsLoggedIn { get; set; }
        public int RoleId { get; set; }
        public RolesViewModel Role { get; set; }
        public int CustomerLevelId { get; set; }
        public CustomerLevelViewModel Level { get; set; }
       List<RewardPointHistoryViewModel> RewardPointHistories { get; set; }
    }
}