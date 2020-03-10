using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoyaltyProgram.ViewModels
{
    public class RolesViewModel
    {
        public int RoleId { get; set; }

        public String RoleName { get; set; }

        public String RoleDescription { get; set; }
        //public ICollection<User> Users { get; set; }
        //public ICollection<Customer> Customers { get; set; }
    }
}