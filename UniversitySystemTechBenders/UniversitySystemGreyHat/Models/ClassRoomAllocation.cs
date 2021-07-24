using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversitySystemTechBenders.Models
{
    public class ClassRoomAllocation
    {
        public int ClassRoomAllocationId { get; set; }
        [Required(ErrorMessage = "Please select a Course")]

        public int CourseId { get; set; }
        [Required(ErrorMessage = "Please select a Room")]

        public int RoomId { get; set; }
        [Required(ErrorMessage = "Please select a Day")]

        public int DayId { get; set; }
        [Required(ErrorMessage = "Please provide a from time")]

        public string FromTime { get; set; }
        [Required(ErrorMessage = "Please provide a from time")]

        public string ToTime { get; set; }
        public string Status { get; set; }
    }
}