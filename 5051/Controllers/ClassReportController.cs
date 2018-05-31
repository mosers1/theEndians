using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _5051.Controllers
{
    public class ClassReportController : Controller
    {
        /// <summary>
        /// Directs the user to the page where they can see a class attendance data report.
        /// </summary>
        /// <returns></returns>
        // GET: ClassReport
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        // Method to Go Back to previous page which is Admin by sitemap design
        /// </summary>
        /// <returns></returns>
        // GET: ClassReport
        public ActionResult GoBack()
        {
            return RedirectToAction("Index", "Admin");
        }
    }
}
