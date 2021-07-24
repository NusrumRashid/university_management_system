using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystemTechBenders.Gateway;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Manager
{
    
    public class CourseManager
    {
        private CourseGateway courseGateway;

        public CourseManager()
        {
            courseGateway=new CourseGateway();
        }

        public string Save(Course course)
        {
            bool isExists = courseGateway.IsCourseExists(course);
            if (isExists)
            {
                return "Course Code Already Exists";
            }
            else if (courseGateway.IsCourseCodeExists(course))
            {
                return "Course Code Already Exists";
                
            }
           
            else
            {


                int rowAffect = courseGateway.Save(course);
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
            return courseGateway.GetAllDepartments();
        }
        public List<Semester> GetAllSemesters()
        {
            return courseGateway.GetAllSemesters();
        }
        public List<SelectListItem> GetDepartmentsForDropDown()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            selectListItems.Add(new SelectListItem() { Text = "--Select a department--", Value = "" });
            foreach (Department department in GetAllDepartments())
            {
                SelectListItem selectList = new SelectListItem();
                selectList.Text = department.DepartmentCode;
                selectList.Value = department.DepartmentId.ToString();
                selectListItems.Add(selectList);

            }
            return selectListItems;
        }

        public List<SelectListItem> GetSemestersForDropDown()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            selectListItems.Add(new SelectListItem() { Text = "--Select a semster--", Value = "" });
            foreach (Semester semester in GetAllSemesters())
            {
                SelectListItem selectList = new SelectListItem();
                selectList.Text = semester.Name;
                selectList.Value = semester.SemesterId.ToString();
                selectListItems.Add(selectList);

            }
            return selectListItems;
        }
        public string UnAssign()
        {
            bool isExists1 = courseGateway.IsCourseUnAssigned1();
            bool isExists2 = courseGateway.IsCourseUnAssigned2();
            bool isExists3 = courseGateway.IsCourseUnAssigned3();
            if (isExists1 && isExists2 && isExists3)
            {
                return "Coures already UnAssigned";
            }
            else
            {
                int rowAffect = courseGateway.UnAssign();
                if (rowAffect > 0)
                {
                    return "UnAssign Succesful";
                }
                else
                {
                    return "UnAssign Failed";
                }
            }
        }
    }
}