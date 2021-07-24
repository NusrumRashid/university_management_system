using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversitySystemTechBenders.Models
{
    public class Login
    {
        public int LoginId { get; set; }

        [Required(ErrorMessage = "Enter UserName")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        
        public string Password { get; set; }
    }
}