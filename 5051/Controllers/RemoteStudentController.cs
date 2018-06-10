using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;

namespace _5051.Controllers
{
    /// <summary>
    /// Remote student controller handles the landing page the student will see
    /// after logging in remotely.
    /// </summary>
    public class RemoteStudentController : Controller
    {
        // A ViewModel used for the Student that contains the StudentList
        private StudentViewModel StudentViewModel = new StudentViewModel();

        // The Backend Data source
        private StudentBackend StudentBackend = StudentBackend.Instance;
        /// <summary>
        /// Render page of student report with the first student in student list
        /// queried for student data on page
        /// </summary>
        /// <returns>Report Page</returns>
        /// GET: /RemoteStudent/Report
        public ActionResult Report()
        {
            // Query backend to refresh every time Index() is called
            var myDataList = StudentBackend.Index();
            var StudentViewModel = new StudentViewModel(myDataList);
            var StudentModel = new StudentModel(StudentViewModel.StudentList[0]);

            return View(StudentModel);
        }
        /// <summary>
        /// Renders page of all available achievements
        /// </summary>
        /// <returns>Achievement Page</returns>
        /// GET: /RemoteStudent/Achievements
        public ActionResult Achievements()
        {
            return View();
        }

        /// <summary>
        /// Render a list of avatars queried from the backend
        /// </summary>
        /// <returns>Choose Avatar View Page</returns>
        /// GET: /RemoteStudent/ChooseAvatar
        public ActionResult ChooseAvatar()
        {
            
            // Query backend to refresh every time Index() is called
            var myDataList = StudentBackend.Index();
            var StudentViewModel = new StudentViewModel(myDataList);
            var StudentModel = new StudentModel(StudentViewModel.StudentList[0]);
            
            return View(StudentModel);
        }

                         /// NOTE: Scott, thanks for the code
        /// <summary>
        /// Choose avatar and update the student model
        /// </summary>
        /// <param name="data"></param>
        /// <returns>A redirect to the updated report</returns>
        // POST: Student/ChooseAvatar
        [HttpPost]
        public ActionResult ChooseAvatar([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "AvatarId,"+
                                        "IsActive,"+
                                        "IsEdit,"+
                                        "LoginStatus,"+
                                        "DailyStatus,"+
                                        "TimeIn,"+
                                        "TimeOut,"+
                                        "Username,"+
                                        "")] StudentModel data)
        {
            
            if (!ModelState.IsValid)
            {
                // Send back for edit
                return View(data);
            }

            if (data == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
                
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Return back for Edit
                return View(data);
            }

            // Make it official
            StudentBackend.Update(data);

            return RedirectToAction("Report");
        }


        /// <summary>
        /// Looks up a student in the backend and sends to the view to render 
        /// the student history report.
        /// </summary>
        /// <param name="id"></param> Student ID
        /// <returns></returns>
        // GET: RemoteStudent/StudentHistory/
        public ActionResult StudentHistory(string id = null)
        {
            // Query our backend backend
            var myDataList = StudentBackend.Index();
            var StudentViewModel = new StudentViewModel(myDataList);

            // Attempt to lookup student if we received an ID
            if (!string.IsNullOrEmpty(id))
            {
                foreach (var item in StudentViewModel.StudentList)
                {
                    if (id == item.Id)
                    {
                        // We found the correct student so send up the model
                        return View(item);
                    }
                }
            }

            // Default to sending up no model
            return View();
        }
    }
}