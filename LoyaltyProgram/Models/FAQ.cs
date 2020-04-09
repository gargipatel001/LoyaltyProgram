    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LoyaltyProgram.Models
{
    [Table("FAQ")]
    public class FAQ
    {
        [Key]
        public int FAQId { get; set; }
     
        public String Question { get; set; }

        public String Answer { get; set; }
        [DefaultValue("true")]

        public bool IsActive { get; set; }
    }
}