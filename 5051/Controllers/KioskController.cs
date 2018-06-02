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
        // A ViewModel used for the Student that contains the StudentList
        private StudentViewModel StudentViewModel = new StudentViewModel();

        // The Backend Data source
        private StudentBackend StudentBackend = StudentBackend.Instance;

        /// <summary>
        /// Return the list of students with the status of logged in or out
        /// </summary>
        /// <returns></returns>
        // GET: Kiosk
        public ActionResult Index()
        {
            // Query backend to refresh every time Index() is called
            var myDataList = StudentBackend.Index();
            var StudentViewModel = new StudentViewModel(myDataList);
            return View(StudentViewModel);
        }

        /// <summary>
        /// This function toggles the student's checkin status (from in->out or out->in).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Kiosk/Update/5
        public ActionResult Update(string id = null)
        {
            // Verify parameter integrity
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home", "Invalid Data");
            }

            // Query backend for student data
            StudentModel myStudent = StudentBackend.Read(id);

            // Switch the student's login status
            if (myStudent.LoginStatus == StudentLoginStatusEnum.In)
            {
                // Sign-in the student
                myStudent.LoginStatus = StudentLoginStatusEnum.Out;
                myStudent.TimeOut = DateTime.Now.ToString(@"h\:mmtt");
                // TODO: Probably a better way/place to handle this logic.
                // Deferring any changes to a later release.
                myStudent.DailyStatus = StudentDailyStatusEnum.Present;
            } else
            {
                // Sign-out the student
                myStudent.LoginStatus = StudentLoginStatusEnum.In;
                myStudent.TimeIn = DateTime.Now.ToString(@"h\:mmtt");
            }

            // Update the backend
            StudentBackend.Update(myStudent);

            // Call the index
            return RedirectToAction("Index");
        }        
    }
}