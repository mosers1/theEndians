using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;

namespace _5051.Controllers
{
    public class RemoteStudentController : Controller
    {
        // A ViewModel used for the Student that contains the StudentList
        private StudentViewModel StudentViewModel = new StudentViewModel();

        // The Backend Data source
        private StudentBackend StudentBackend = StudentBackend.Instance;
        // GET: /AdminPanel/Options/someName
        public ActionResult Report()
        {
            // Query backend to refresh every time Index() is called
            var myDataList = StudentBackend.Index();
            var StudentViewModel = new StudentViewModel(myDataList);
            var StudentModel = new StudentModel(StudentViewModel.StudentList[0]);

            return View(StudentModel);
        }
        //Return Achievements page
        public ActionResult Achievements()
        {
            return View();
        }
        
        //Returns Avatar select page
        public ActionResult ChooseAvatar()
        {
            
            // Query backend to refresh every time Index() is called
            var myDataList = StudentBackend.Index();
            var StudentViewModel = new StudentViewModel(myDataList);
            var StudentModel = new StudentModel(StudentViewModel.StudentList[0]);
            
            return View(StudentModel);
        }

                         // NOTE: Scott, thanks for the code
        /// <summary>
        /// Choose avatar and update the student model
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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
 
        //public ActionResult ChooseAvatar()
       // {
         //   
        //}
        //Returns student history page
        public ActionResult StudentHistory()
        {
            return View();
        }
    }
}