using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LoyaltyProgram.Models
{
    [Table("Roles")]
    public class Roles
    {
     
        [Key]
        public int RoleId { get; set; }
        
        public String RoleName { get; set; }

        public String RoleDescription { get; set; }
        public ICollection<User> Users { get; set; }
        [DefaultValue("true")]
        public bool IsActive { get; set; }
        public ICollection<Customer> Customers { get; set; }

    }
}