using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystemTechBenders.Gateway;
using UniversitySystemTechBenders.Manager;
using UniversitySystemTechBenders.Models;
using UniversitySystemTechBenders.Models.ViewModel;

namespace UniversitySystemTechBenders.Controllers
{
    public class AssignCourseToTeacherController : Controller
    {
        private DepartmentManager departmentManager;
        private CourseManager courseManager;
        private AssignCourseToTeacherManager assignCourseToTeacherManager;

        public AssignCourseToTeacherController()
        {
            departmentManager=new DepartmentManager();
            courseManager=new CourseManager();
            assignCourseToTeacherManager=new AssignCourseToTeacherManager();
        }
        // GET: AssignCourseToTeacher
        

        public JsonResult GetTacherByDepartmentId(int deptId)
        {
           List<TeacherViewModel> teacherViewModels= assignCourseToTeacherManager.GetTacherByDepartmentId(deptId);
            JsonResult jsonResult = Json(teacherViewModels);
            return jsonResult;

        }

        public JsonResult GetCredit(int teachrId)
        {
            TeacherViewModel teacherViewModel=assignCourseToTeacherManager.GetCredit(teachrId);
            JsonResult jsonResult = Json(teacherViewModel);
            return jsonResult;

        }
        public JsonResult GetTakenCredit(int teachrId)
        {
          
            TeacherViewModel takenCredit = assignCourseToTeacherManager.GetTakenCredit(teachrId);
            JsonResult jsonResult = Json(takenCredit);
            return jsonResult;

        }

        public ActionResult CourseStatistics()
        {
            ViewBag.Departments = courseManager.GetDepartmentsForDropDown();
            return View();
        }

        public JsonResult ViewCourseStatistics(int depttId)
        {
            List<CourseStatisticsViewModel> courseStatistics = assignCourseToTeacherManager.ViewCourseStatistics(depttId);
            JsonResult jsonResult = Json(courseStatistics);
            return jsonResult;
        }

        public JsonResult GetCourseByDepartmentId(int departId)
        {
            List<Course> courseList = assignCourseToTeacherManager.GetCourseByDepartmentId(departId);
            JsonResult jsonResult = Json(courseList);
            return jsonResult;

        }

        public JsonResult GetCourseByCourseId(int courseId)
        {
            Course courseList = assignCourseToTeacherManager.GetCourseByCourseId(courseId);
            JsonResult jsonResult = Json(courseList);
            return jsonResult;

        }

        [HttpGet]
        public ActionResult Save()
        {

            ViewBag.Departments = courseManager.GetDepartmentsForDropDown();
            return View();
        }

        [HttpPost]
        public ActionResult Save(AssignCourseToTeacher assign)
        {
            if (ModelState.IsValid)
            {
                assign.Status = "Assigned";
                string message = assignCourseToTeacherManager.Save(assign);
                ViewBag.Departments = courseManager.GetDepartmentsForDropDown();
                ViewBag.Message = message;
            }
            else
            {
                ViewBag.Departments = courseManager.GetDepartmentsForDropDown();
                ViewBag.Message = "Model State is Invalid";
                return View();
            }
           
                return View();
            }
           
    }
}