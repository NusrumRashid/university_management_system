using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversitySystemTechBenders.Models
{
    public class EnrollCourse
    {
        public int EnrollCourseId { get; set; }
        
        [Required(ErrorMessage = "Please provide your Student")]
        public int StudentId { get; set; }

        public int CourseId { get; set; }

        [Required(ErrorMessage = "Please provide Date")]
        [DisplayName("Date")]
        public string Date { get; set; }

        public string Status { get; set; }
        
  
    }
    }
