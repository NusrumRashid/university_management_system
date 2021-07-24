using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystemTechBenders.Gateway;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Manager
{
    public class TeacherManager
    {
        private TeacherGateway teacherGateway;
        private DepartmentGateway departmentGateway;

        public TeacherManager()
        {
            teacherGateway = new TeacherGateway();
            departmentGateway = new DepartmentGateway();
        }

        public string Save(Teacher teacher)
        {
            int rowAffected = teacherGateway.UpdateCredit(teacher);
            bool isExists = teacherGateway.IsEmailContactExists(teacher);
            if (isExists)
            {
              
                if (rowAffected > 0)
                {
                    return "Successfully Update credit";
                }
                else
                {
                    return "Update Failed";
                }
            }
            else if (teacherGateway.IsEmailExists(teacher))
            {
                return "Email Already Exists";
                
            }
            else if (teacherGateway.IsContactExists(teacher))
            {
                return "Contact Already Exists";
                
            }
            else
            {
                int rowAffect = teacherGateway.Save(teacher);

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
    

    public List<Department> GetAllDepartments()
        {
            return departmentGateway.GetAllDepartments();
        }
        public List<Designation> GetAllDesignations()
        {
            return teacherGateway.GetAllDesignations();
        }
        public List<SelectListItem> GetDepartmentsForDropDown()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            selectListItems.Add(new SelectListItem() { Text = "--Select a Department--", Value = "" });
            foreach (Department department in GetAllDepartments())
            {
                SelectListItem selectList = new SelectListItem();
                selectList.Text = department.DepartmentCode;
                selectList.Value = department.DepartmentId.ToString();
                selectListItems.Add(selectList);

            }
            return selectListItems;
        }

        public List<SelectListItem> GetDesignationsForDropDown()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            selectListItems.Add(new SelectListItem() { Text = "--Select a Designation--", Value = "" });
            foreach (Designation designation in GetAllDesignations())
            {
                SelectListItem selectList = new SelectListItem();
                selectList.Text = designation.DesignationName;
                selectList.Value = designation.DesignationId.ToString();
                selectListItems.Add(selectList);

            }
            return selectListItems;
        }
    }
}