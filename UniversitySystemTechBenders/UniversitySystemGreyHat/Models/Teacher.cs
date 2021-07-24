using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversitySystemTechBenders.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "Please provide Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please provide Email")]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Contact No")]
        [Required(ErrorMessage = "Please provide Contact No.")]
        [MaxLength(11, ErrorMessage = "Contact number must be 11 Characters and numeric")]
        [MinLength(11, ErrorMessage = "Contact number must be 11 Characters and numeric")]
         public string ContactNumber { get; set; }
       
       
        [DisplayName("Designation")]
        [Required(ErrorMessage = "Please select Designation")]
        public int DesignationId { get; set; }
        [DisplayName("Department")]
        [Required(ErrorMessage = "Please select Department")]
        public int DepartmentId { get; set; }

        [DisplayName("Credit To Be Taken")]
        [Required(ErrorMessage = "Please provide Credit to be Taken")]
        [Range(0.5, 100.00, ErrorMessage = "Credit cannot be Negative")]
        public float TakenCredit { get; set; }
    }
}