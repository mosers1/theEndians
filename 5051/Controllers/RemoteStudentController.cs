using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _5051.Controllers
{
    public class RemoteStudentController : Controller
    {
        // GET: /AdminPanel/Options
        public ActionResult Report()
        {
            ViewBag.Message = "View student report online (not via kiosk)";
            return View();
        }
    }
}