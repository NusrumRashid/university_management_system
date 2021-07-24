using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversitySystemTechBenders.Gateway;
using UniversitySystemTechBenders.Models;
using UniversitySystemTechBenders.Models.ViewModel;

namespace UniversitySystemTechBenders.Manager
{
    public class AssignCourseToTeacherManager
    {
        private AssignCourseToTeacherGateway assignGateway;

        public AssignCourseToTeacherManager()
        {
            assignGateway = new AssignCourseToTeacherGateway();
        }

        public List<TeacherViewModel> GetTacherByDepartmentId(int departmentId)
        {
            return assignGateway.GetTacherByDepartmentId(departmentId);
        }

        public TeacherViewModel GetTakenCredit(int teacherId)
        {
            return assignGateway.GetTakenCredit(teacherId);
        }

        public TeacherViewModel GetCredit(int teacherId)
        {
            return assignGateway.GetCredit(teacherId);
        }

        public List<CourseStatisticsViewModel> ViewCourseStatistics(int departmentId)
        {
            return assignGateway.ViewCourseStatistics(departmentId);
        }

        public List<Course> GetCourseByDepartmentId(int departmentId)
        {
            return assignGateway.GetCourseByDepartmentId(departmentId);
        }

        public Course GetCourseByCourseId(int courseId)
        {
            return assignGateway.GetCourseByCourseId(courseId);
        }

        public string Save(AssignCourseToTeacher assign)
        {

            if (assignGateway.IsCourseAssigned(assign))
            {
                return "Course Already Assigned";
            }
           
            else
            {


                int rowAffect = assignGateway.Save(assign);
                if (rowAffect > 0)
                {
                    return "Save Succesful";
                }
                else
                {
                    return "Save Failed";
                }
            }
        }
    }



}