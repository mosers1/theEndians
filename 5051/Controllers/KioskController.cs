using _5051.Backend;
using _5051.Models;
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
        private StudentCheckinViewModel viewModel = new StudentCheckinViewModel();

        // The Backend Data source
        private StudentCheckinBackend backend = StudentCheckinBackend.Instance;

        /// <summary>
        /// Return the list of students with the status of logged in or out
        /// </summary>
        /// <returns></returns>
        // GET: Kiosk
        public ActionResult Index()
        {
            viewModel.CheckinList = backend.Index();
            return View(viewModel);
        }

        /// <summary>
        /// This should check in or out
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Kiosk/Update/5
        public ActionResult Update(string id = null)
        {
            var myData = backend.Read(id);
            myData.CheckedIn = !myData.CheckedIn;
            return View(myData);
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
        
    }
}