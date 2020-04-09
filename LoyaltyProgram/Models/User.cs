using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LoyaltyProgram.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public String UserEmail { get; set; }
        public String UserPassword { get; set; }
        [StringLength(25)]
        public String UserFirstName { get; set; }
        [StringLength(25)]
        public String UserLastName { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool isLoggedIn { get; set; }
      
        public int RoleId { get; set; }
        [DefaultValue("true")]
        public bool IsActive { get; set; }
        public Roles Role { get; set; }
    }
}