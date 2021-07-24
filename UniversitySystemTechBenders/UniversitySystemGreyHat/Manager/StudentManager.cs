using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementWebApp.Models;
using UniversityManagementWebApp.Models.ViewModel;
using UniversitySystemTechBenders.Gateway;

namespace UniversityManagementWebApp.Manager
{
    public class StudentManager
    {
        StudentGateway studentGateway=new StudentGateway();

        public List<StudentViewModel> GetAllDepartments()
        {
            return studentGateway.GetAllDepartments();
        }

        public StudentModel IdGenerate(string department, int departmentId)
        {
            return studentGateway.IdGenerate(department, departmentId);
        }

        public List<StudentViewModel> GetStudentInfo(StudentModel studentModel)
        {
            return studentGateway.GetStudentInfo(studentModel);
        }
        public string Save(StudentModel studentModel)
        {
            if (studentGateway.IsRegistrationExist(studentModel))
            {
                return "Registration No Already Exist";
            }
            else if (studentGateway.IsEmailExist(studentModel))
            {
                return "Email Already Exist";

            }
            else if (studentGateway.IsContactExist(studentModel))
            {
                return "Contact Number Already Exist";
                
            }
            else
            {
                int rowAffect = studentGateway.Save(studentModel);
                if (rowAffect>0)
                {
                    return "Save Successfully";
                }
                else
                {
                    return "Save failed";
                }
            }
        }
    }
}