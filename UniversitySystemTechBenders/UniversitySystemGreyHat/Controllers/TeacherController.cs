using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystemTechBenders.Manager;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Controllers
{
    public class TeacherController : Controller
    {
        private TeacherManager teacherManager;

        public TeacherController()
        {
            teacherManager=new TeacherManager();
        }
        // GET: Teacher
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult Save()
        {
            ViewBag.designations = teacherManager.GetDesignationsForDropDown();
            ViewBag.departments = teacherManager.GetDepartmentsForDropDown();
          

            return View();
        }

        [HttpPost]
        public ActionResult Save(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                string message = teacherManager.Save(teacher);
                ViewBag.Message = message;
                ViewBag.departments = teacherManager.GetDepartmentsForDropDown();
                ViewBag.designations = teacherManager.GetDesignationsForDropDown();
                
            }
            else
            {
                ViewBag.departments = teacherManager.GetDepartmentsForDropDown();
                ViewBag.designations = teacherManager.GetDesignationsForDropDown();
                ViewBag.Message = "Model State is Invalid";
                return View();
            }
                return View();
        }
    }
}