using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _5051.Controllers
{
    public class ClassReportController : Controller
    {
        // GET: ClassReport
        public ActionResult Index()
        {
            return View();
        }

        // Method to Go Back to previous page which is AdminPanel by design
        // GET: ClassReport
        public ActionResult GoBack()
        {
            return RedirectToAction("Index", "Admin");
        }
    }
}
