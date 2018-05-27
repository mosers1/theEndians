using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _5051.Controllers
{
    public class RemoteStudentController : Controller
    {
        // GET: /AdminPanel/Options/someName
        public ActionResult Report(String username = null)
        {
            if (username == null) username = "Student"; 
            ViewBag.Message = "Welcome " + username + "!";
            return View();
        }

        public ActionResult Achievements()
        {
            return View();
        }

        public ActionResult ChooseAvatar()
        {
            return View();
        }

        public ActionResult StudentHistory()
        {
            return View();
        }
    }
}