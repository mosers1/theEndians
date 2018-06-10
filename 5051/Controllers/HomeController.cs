using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _5051.Controllers
{
    /// <summary>
    /// The home controller is the first page called via the website URL.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// This function gets called by default when the website is accessed.
        /// </summary>
        /// <returns></returns>
        // GET: Home/Index/
        public ActionResult Index()
        {
            return View();
        }
    }
}