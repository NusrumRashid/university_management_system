using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystemTechBenders.Manager;
using UniversitySystemTechBenders.Models;
using UniversitySystemTechBenders.Models.ViewModel;

namespace UniversitySystemTechBenders.Controllers
{
    public class ClassRoomAllocationController : Controller
    {
        
        private CourseManager courseManager;
        private ClassRoomAllocationManager classRoomAllocationManager;
        private AssignCourseToTeacherManager assignCourseToTeacherManager;
        private DayManager dayManager;
        private RoomManager roomManager;

        public ClassRoomAllocationController()
        {
            courseManager=new CourseManager();
            classRoomAllocationManager=new ClassRoomAllocationManager();
            assignCourseToTeacherManager=new AssignCourseToTeacherManager();
            dayManager=new DayManager();
            roomManager=new RoomManager();
        }
        // GET: ClassRoomAllocation

        [HttpGet]
        public ActionResult Save()
        {
            ViewBag.Days = dayManager.GetDaysForDropDown();
            ViewBag.Rooms = roomManager.GetRoomsForDropDown();
            ViewBag.Departments = courseManager.GetDepartmentsForDropDown();
            return View();
        }

        [HttpPost]
        public ActionResult Save(ClassRoomAllocation allocation)
        {
            if (ModelState.IsValid)
            {
                allocation.Status = "Allocated";
                string message = classRoomAllocationManager.Save(allocation);
                ViewBag.Message = message;
                ViewBag.Days = dayManager.GetDaysForDropDown();
                ViewBag.Rooms = roomManager.GetRoomsForDropDown();
                ViewBag.Departments = courseManager.GetDepartmentsForDropDown();
            }
            else
            {
                ViewBag.Days = dayManager.GetDaysForDropDown();
                ViewBag.Rooms = roomManager.GetRoomsForDropDown();
                ViewBag.Departments = courseManager.GetDepartmentsForDropDown();
                ViewBag.Message = "Model State is Invalid";
                return View();
            }
            
            
            return View();
        }

        public JsonResult GetCourseByDepartmentId(int departId)
        {
            List<Course> courseList = assignCourseToTeacherManager.GetCourseByDepartmentId(departId);
            JsonResult jsonResult = Json(courseList);
            return jsonResult;

        }
        [HttpGet]
        public ActionResult ViewClassSchedule()
        {
            ViewBag.Departments = courseManager.GetDepartmentsForDropDown();
           
            return View();
        }
        public JsonResult GetClassRoutineByDepartmentId(int departId)
        {
            List<ClassAllocationViewModel> classAllocationViewModels = classRoomAllocationManager.ViewClassScheduleInfoByDepartmentId(departId);
            JsonResult jsonResult = Json(classAllocationViewModels);
            return jsonResult;

        }
    }
}