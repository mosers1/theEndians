using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _5051.Controllers
{
    public class AdminPanelController : Controller
    {
        // GET: /AdminPanel/Options
        public ActionResult Options()
        {
            ViewBag.Message = "Admin Options Panel";
            return View();
        }

        // GET: /AdminPanel/Launch
        public ActionResult Launch()
        {
            return RedirectToAction("Index", "Kiosk");
        }
    }
}