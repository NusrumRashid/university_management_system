using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;

namespace UniversityManagementWebApp.Models
{
    public class StudentModel
    {
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Please provide registration no.")]
        [StringLength(16, MinimumLength = 10, ErrorMessage = "Contact number must be 11 Characters")]

        public string RegistrationNo { get; set; }
        [Required(ErrorMessage = "Please provide your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please provide Email")]
        [EmailAddress]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Must be at least 8 characters long.")]
        public string Email { get; set; }
        [DisplayName("Contact No")]
        [Required(ErrorMessage = "Please provide Contact No.")]
        [MaxLength(11, ErrorMessage = "Contact number must be 11 Characters and numeric")]
        [MinLength(11, ErrorMessage = "Contact number must be 11 Characters and numeric")]
       public string ContactNo { get; set; }

        [Required(ErrorMessage = "Please provide Date")]
        [DisplayName("Date")]
        
        public string Date { get; set; }
        [DisplayName("Address")]
        [Required(ErrorMessage = "Please provide address")]
       

        public string Address { get; set; }

         [Required(ErrorMessage = "Please select department")]
        public int DepartmentId { get; set; }

    }
}