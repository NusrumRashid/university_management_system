using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementWebApp.Models.ViewModel;
using UniversitySystemTechBenders.Models.ViewModel;
using UniversitySystemTechBenders.Manager;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Controllers
{
    public class EnrollCourseController : Controller
    {
        //
        // GET: /EnrollCourse/
        private EnrollCourseManager enrollCourseManager;

        public EnrollCourseController()
        {
            enrollCourseManager=new EnrollCourseManager();
        }
        [HttpGet]
        public ActionResult Save()
        {
            ViewBag.Students = enrollCourseManager.GetStudentsForDropDown();
            return View();
        }

        [HttpPost]
        public ActionResult Save(EnrollCourse enroll)
        {
            if (ModelState.IsValid)
            {
                enroll.Status = "Allocate";
                string message = enrollCourseManager.Save(enroll);
                ViewBag.Message = message;
                ViewBag.Students = enrollCourseManager.GetStudentsForDropDown();
            }
            else
            {
                ViewBag.Students = enrollCourseManager.GetStudentsForDropDown();
                return View();
            }
           
            return View();
          
        }

        public ActionResult GetStudentById(int studentId)
        {
            StudentViewModel studentViewModel = enrollCourseManager.GetStudentById(studentId);

            JsonResult jsonResult = Json(studentViewModel);
            return jsonResult;
        }

        public ActionResult GetCourseByDepartmentCode(string registrationNo)
        {
            List<StudentViewModel> studentViewModel = enrollCourseManager.GetCourseByDepartmentCode(registrationNo);
            JsonResult jsonResult = Json(studentViewModel);
            return jsonResult;
        }

    
    }
}