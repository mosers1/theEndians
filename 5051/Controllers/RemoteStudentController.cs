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