using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _5051.Controllers
{
    /// <summary>
    /// The kiosk student check in/out screen that will run in the classroom
    /// </summary>
    public class KioskController : Controller
    {

        /// <summary>
        /// Return the list of students with the status of logged in or out
        /// </summary>
        /// <returns></returns>
        // GET: Kiosk
        public ActionResult Index()
        {
            // TODO: Add logic...
            return View();
        }

        public ActionResult SignedIn()
        {
            ViewBag.Message = "SignedIn";
            return View();
        }

        public ActionResult SignedOut()
        {
            ViewBag.Message = "SignedOut";
            return View();
        }

        // GET: Kiosk/SetLogout/5
        public ActionResult SetLogin(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home", "Invalid Data");
            }

            // TODO: Do something...

            return RedirectToAction("Index");
        }

        // GET: Kiosk/SetLogout/5
        public ActionResult SetLogout(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home", "Invalid Data");
            }

            // TODO: Do something...

            return RedirectToAction("Index");
        }

        public ActionResult startLogin()
        {
            return RedirectToAction("Options", "AdminPanel");
        }

        public ActionResult checkIn()
        {
            return RedirectToAction("SignedIn", "Kiosk");
        }

        public ActionResult checkOut()
        {
            return RedirectToAction("SignedOut", "Kiosk");
        }
    }
}