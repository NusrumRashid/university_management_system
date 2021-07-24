using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementWebApp.Manager;
using UniversityManagementWebApp.Models.ViewModel;
using UniversitySystemTechBenders.Manager;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Controllers
{
    public class StudentResultController : Controller
    {
        private StudentResultManager studentResultManager;
        private GradeLetterManager gradeLetterManager;
        private EnrollCourseManager enrollCourseManager;

        public StudentResultController()
        {
            studentResultManager=new StudentResultManager();
            gradeLetterManager=new GradeLetterManager();
            enrollCourseManager=new EnrollCourseManager();
        }
        [HttpGet]
        public ActionResult Save()
        {
            ViewBag.Students = enrollCourseManager.GetStudentsForDropDown();
            ViewBag.GradeLetters = gradeLetterManager.GetGradesForDropDown();
            return View();
        }

        [HttpPost]
        public ActionResult Save(StudentResult result)
        {
            if (ModelState.IsValid)
            {
                string message = studentResultManager.Save(result);
                ViewBag.Message = message;
                ViewBag.GradeLetters = gradeLetterManager.GetGradesForDropDown();
                ViewBag.Students = enrollCourseManager.GetStudentsForDropDown();
            }
            else
            {
                ViewBag.GradeLetters = gradeLetterManager.GetGradesForDropDown();
                ViewBag.Students = enrollCourseManager.GetStudentsForDropDown();
          
                return View();
            }

            
            return View();

        }
        public ActionResult GetCourseById(int studtId)
        {
            List<StudentViewModel> studentViewModel = studentResultManager.GetCourseById(studtId);
            JsonResult jsonResult = Json(studentViewModel);
            return jsonResult;
        }
 
        public ActionResult ViewResult()
        {
            ViewBag.Students = enrollCourseManager.GetStudentsForDropDown();

            return View();
        }
        public ActionResult GetResult(int studenId)
        {
            List<StudentViewModel> studentViewModel = studentResultManager.GetResultList(studenId);
            JsonResult jsonResult = Json(studentViewModel);
            return jsonResult;
        }

    }
}