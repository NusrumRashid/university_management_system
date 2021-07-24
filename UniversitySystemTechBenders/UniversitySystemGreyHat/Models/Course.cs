using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversitySystemTechBenders.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Please provide Code")]
        [RegularExpression("^.*(?=.*[A-Z])(?=.*[0-9]).*$", ErrorMessage = "Must enter one Capital letter and numeric value")]

        [StringLength(10, MinimumLength = 5, ErrorMessage = "Code must be at least 5 Characters Long")]
        public string Code { get; set; }
        [RegularExpression("^.*(?=.*[A-Z]).*$", ErrorMessage = "Must enter one Capital letter ")]
        [Required(ErrorMessage = "Please provide Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide Credit")]
        [Range(0.5,5.0, ErrorMessage = "Enter credit should be between 0.5 to 5.0")]
        public float Credit  { get; set; }

        [Required(ErrorMessage = "Please provide Description")]
        public string Description { get; set; }

        [DisplayName("DepartmentId")]
        [Required(ErrorMessage = "Please select Department")]
        public int DepartmentId { get; set; }

        [DisplayName("Semester")]
        [Required(ErrorMessage = "Please select Semester")]
        public int SemesterId { get; set; }
        public string Status { get; set; }
    }
}