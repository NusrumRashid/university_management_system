using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystemTechBenders.Manager;

namespace UniversitySystemTechBenders.Controllers
{
    public class UnassignCoursesController : Controller
    {
        private CourseManager courseManager;

        public UnassignCoursesController()
        {
            courseManager=new CourseManager();
        }

        [HttpGet]
        public ActionResult Unassign()
        {
            
            return View();

        }

        [HttpPost]
        public ActionResult Unassign(CourseManager course)
        {
            if (ModelState.IsValid)
            {
                string message = courseManager.UnAssign();
                ViewBag.Message = message;
            }
            else
            {


                return View();
            }
           
            return View();
        }
    }
}