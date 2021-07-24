using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementWebApp.Models.ViewModel
{
    public class StudentViewModel
    {
        [Required(ErrorMessage = "Please provide your Student")]
        public int StudentId { get; set; }
        public string RegistrationNo { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }

  
        public string Date { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }

        [Required(ErrorMessage = "Please provide your Course")]
        public int CourseId { get; set; }

        public string CourseCode { get; set; }
        public string CourseName { get; set; }

         [Required(ErrorMessage = "Please provide your GradeLetter")]
        public int GradeLetterId { get; set; }

        public string Grade { get; set; }

    }
}