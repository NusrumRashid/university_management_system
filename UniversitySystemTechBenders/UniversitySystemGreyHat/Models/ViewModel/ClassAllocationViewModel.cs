using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversitySystemTechBenders.Models.ViewModel
{
    public class ClassAllocationViewModel
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int RoomId { get; set; }
        public string RoomNo { get; set; }
        public int DayId { get; set; }
        public string DayName { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string ScheduleInfo { get; set; }


        
    }
}