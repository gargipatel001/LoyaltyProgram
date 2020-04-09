using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LoyaltyProgram.Models
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        public String CustomerEmail { get; set; }
        public String CustomerPassword { get; set; }
        [StringLength(25)]
        public String CustomerFirstName { get; set; }
        [StringLength(25)]
        public String CustomerLastName { get; set; }
        public String CustomerAddress
        { get; set; }
        [StringLength(25)]
        public String CustomerCity
        { get; set; }
        [StringLength(25)]
        public String CustomerProvince { get; set; }
        [StringLength(6)]
        public String CustomerPostalCode
        { get; set; }

        public DateTime? CustomerDOB { get; set; }
        [StringLength(10)]
        public String CustomerPhoneNumber { get; set; }
        [StringLength(1)]
        public String CustomerGender { get; set; }
        [StringLength(16)]
        public String CustomerCardNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Double CustomerLoyaltyPoints { get; set; }
        [DefaultValue("false")]
        public bool IsLoggedIn { get; set; }
        public int RoleId { get; set; }
        public Roles Role { get; set; }
        public int LevelId { get; set; }
        [DefaultValue("true")]
        public bool IsActive { get; set; }
        public CustomerLevel Level { get; set; }
        ICollection<RewardPointHistory> RewardPointHistories { get; set; }


    }
}