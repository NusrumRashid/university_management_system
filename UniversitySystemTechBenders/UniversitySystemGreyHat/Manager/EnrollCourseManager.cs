using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementWebApp.Models;
using UniversityManagementWebApp.Models.ViewModel;
using UniversitySystemTechBenders.Gateway;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Manager
{
    public class EnrollCourseManager
    {
        private EnrollCourseGateway enrollCourseGateway;

        public EnrollCourseManager()
        {
            enrollCourseGateway=new EnrollCourseGateway();
        }
        public List<StudentModel> GetAllRegistrationNo()
        {
            return enrollCourseGateway.GetAllRegistrationNo();
        }

           public List<SelectListItem> GetStudentsForDropDown()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            selectListItems.Add(new SelectListItem() { Text = "--Select a Student--", Value = "" });
            foreach (StudentModel student in GetAllRegistrationNo())
            {
                SelectListItem selectList = new SelectListItem();
                selectList.Text = student.RegistrationNo;
                selectList.Value = student.StudentId.ToString();
                selectListItems.Add(selectList);

            }
            return selectListItems;
        }

        public StudentViewModel GetStudentById(int studentId)
        {
            return enrollCourseGateway.GetStudentById(studentId);
        }

        public List<StudentViewModel> GetCourseByDepartmentCode(string registrationNo)
        {
            return enrollCourseGateway.GetCourseByDepartmentCode(registrationNo);
        }

        public string Save(EnrollCourse enroll)
        {
            bool isExists = enrollCourseGateway.IsCourseEnrolled(enroll);
            if (isExists)
            {
                return "Course already Enrolled for this Student.";
            }

            else
            {


                int rowAffect = enrollCourseGateway.Save(enroll);
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
    }
    }
