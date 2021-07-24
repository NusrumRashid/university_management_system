using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementWebApp.Manager;
using UniversityManagementWebApp.Models;
using UniversityManagementWebApp.Models.ViewModel;
using UniversitySystemTechBenders.Models.ViewModel;

namespace UniversityManagementWebApp.Controllers
{
    public class RegisterStudentController : Controller
    {
        private StudentViewModel studentViewModel;
        private StudentModel studentModel;
        private StudentManager studentManager;

        public RegisterStudentController()
        {
            studentManager = new StudentManager();
            studentModel = new StudentModel();
            studentViewModel = new StudentViewModel();
        }
        //
        // GET: /RegisterStudent/
        [HttpGet]
        public ActionResult Save(string departmentName,StudentViewModel studentViewModel)
        {
            List<StudentViewModel> studentViewModels = studentManager.GetAllDepartments();
            ViewBag.Departments = studentViewModels;
           

            return View();
        }
        [HttpPost]
        public ActionResult Save(StudentModel studentModel)
        {
            if (ModelState.IsValid)
            {
                List<StudentViewModel> studentViewModels = studentManager.GetAllDepartments();
                ViewBag.Departments = studentViewModels;

                ViewBag.Save = studentManager.Save(studentModel);
                ViewBag.StudentInfo = studentManager.GetStudentInfo(studentModel);

            }
            else
            {
                List<StudentViewModel> studentViewModels = studentManager.GetAllDepartments();
                ViewBag.Departments = studentViewModels;
                ViewBag.Message = "Model State is Invalid";
                return View();
            }
            return View();
        }
        public JsonResult GetCredit(string department, int departmentId)
        {
            StudentModel studentViewModels = studentManager.IdGenerate(department, departmentId);
            JsonResult jsonResult = Json(studentViewModels);
            return jsonResult;

        }
    }
}