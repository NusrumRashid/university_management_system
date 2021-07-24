using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementWebApp.Models.ViewModel;
using UniversitySystemTechBenders.Gateway;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Manager
{
    public class StudentResultManager
    {
        private StudentResultGateway studentResultGateway;

        public StudentResultManager()
        {
            studentResultGateway=new StudentResultGateway();
        }
        public string Save(StudentResult result)
        {
            int rowAffected = studentResultGateway.UpdateGrade(result);
            bool isExists = studentResultGateway.IsCourseResultSaved(result);
            if (isExists)
            {

                if (rowAffected > 0)
                {
                    return "Successfully Update Grade";
                }
                else
                {
                    return "Update Failed";
                }
            }

            else
            {


                int rowAffect = studentResultGateway.Save(result);
                if (rowAffect > 0)
                {
                    return "Save Successful";
                }
                else
                {
                    return "Save Failed";
                }
            }
        }

        public List<StudentViewModel> GetCourseById(int studentId)
        {
            return studentResultGateway.GetCourseById(studentId);
        }

        public List<StudentViewModel> GetResultList(int studentId)
        {
            return studentResultGateway.GetResultList(studentId);
        }

    }
}