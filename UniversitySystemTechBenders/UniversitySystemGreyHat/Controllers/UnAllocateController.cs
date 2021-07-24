using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystemTechBenders.Manager;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Controllers
{
    public class UnAllocateController : Controller
    {
        private ClassRoomAllocationManager allocationManager;

        public UnAllocateController()
        {
            allocationManager=new ClassRoomAllocationManager();
        }
        [HttpGet]
        public ActionResult UnAllocate()
        {

            return View();

        }

        [HttpPost]
        public ActionResult UnAllocate(Room room)
        {
            if (ModelState.IsValid)
            {
                string message = allocationManager.UnAllocate();
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