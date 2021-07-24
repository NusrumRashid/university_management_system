using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystemTechBenders.Manager;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Controllers
{
    public class DepartmentController : Controller
    {
        private DepartmentManager departmentManager;

        public DepartmentController()
        {
            departmentManager = new DepartmentManager();
        }

        // GET: Department

        [HttpGet]
        public ActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(Department department)
        {
            if (ModelState.IsValid)
            {
                string message = departmentManager.Save(department);
                ViewBag.Message = message;

            }
            else
            {
                ViewBag.Message = "Model State is Invalid";
                return View();
            }
            return View();
        }
    

    public ActionResult GetAllDepartments()
        {

            List<Department> departmentList = departmentManager.GetAllDepartments();
            return View(departmentList);
        }
    }
}