using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversitySystemTechBenders.Models
{
    public class AssignCourseToTeacher
    {
        public int AssignCourseToTeacherId { get; set; }
        [Required(ErrorMessage = "Please select a department")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Please select a teacher")]

        public int TeacherId { get; set; }
        [Required(ErrorMessage = "Please select a course")]

        public int CourseId { get; set; }
        public string Status { get; set; }
    }
}