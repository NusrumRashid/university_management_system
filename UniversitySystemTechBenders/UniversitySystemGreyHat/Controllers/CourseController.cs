using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystemTechBenders.Manager;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Controllers
{
    public class CourseController : Controller
    {
        private CourseManager courseManager;

        public CourseController()
        {
            courseManager=new CourseManager();
        }
        // GET: Course
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public ActionResult SaveCourse(Course course)
        //{
        //    string message = courseManager.Save(course);
        //    ViewBag.Message = message;
        //    return View();
        //}
        [HttpGet]
        public ActionResult SaveCourse()
        {
            ViewBag.semesters = courseManager.GetSemestersForDropDown();
            ViewBag.departments = courseManager.GetDepartmentsForDropDown();
            
            return View();
        }

        [HttpPost]
        public ActionResult SaveCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                course.Status = "Assigned";
                string message = courseManager.Save(course);
                ViewBag.Message = message;
                ViewBag.departments = courseManager.GetDepartmentsForDropDown();
                ViewBag.semesters = courseManager.GetSemestersForDropDown();
              
            }
            else
            {
                ViewBag.departments = courseManager.GetDepartmentsForDropDown();
                ViewBag.semesters = courseManager.GetSemestersForDropDown();
                ViewBag.Message = "Model State is Invalid";
                return View();
            }
            return View();
        }
    }
}