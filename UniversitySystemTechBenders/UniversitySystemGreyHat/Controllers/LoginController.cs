using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitySystemTechBenders.Manager;
using UniversitySystemTechBenders.Models;

namespace UniversitySystemTechBenders.Controllers
{
    public class LoginController : Controller
    {
        private LoginManager loginManager;

        public LoginController()
        {
            loginManager=new LoginManager();
        }
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                string message = loginManager.IsUserValid(login);
              //  ViewBag.Message = message;
                if (message == "Login Succesful")
                {
                    return RedirectToAction("Save", "Department");
                }
                else
                {
                    ViewBag.Message = "Check email and password";
                }

            }
            else
            {
               
                return View();
            }
            return View(login);


        }
    }
}