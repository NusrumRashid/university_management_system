using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversitySystemTechBenders.Models.ViewModel
{
    public class TeacherViewModel
    {
        public int DepartmentId { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public int TeacherId { get; set; }
        
        public string TeacherName { get; set; }

       
        public string Address { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }
       
        public int DesignationId { get; set; }
      
       
        public double TakenCredit { get; set; }
        public double RemainingCredit { get; set; }


    }
}