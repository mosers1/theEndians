using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;

namespace _5051.Controllers 
{
    public class AdminController : Controller
    {
        // A ViewModel used for the Student that contains the StudentList
        private StudentViewModel StudentViewModel = new StudentViewModel();

        // The Backend Data source
        private StudentBackend StudentBackend = StudentBackend.Instance;

        /// <summary>
        /// Direct to page where user can upload a calendar.
        /// </summary>
        /// <returns></returns>
        // GET: /Admin/Calendar
        public ActionResult Calendar()
        {
            ViewBag.Message = "Calendar Upload";
            return View();
        }

        /// <summary>
        /// Main landing page for this controller.
        /// </summary>
        /// <returns></returns>
        // GET: /Admin/Index
        public ActionResult Index()
        {
            ViewBag.Message = "Admin Index";

            // Query backend to refresh every time Index() is called
            var myDataList = StudentBackend.Index();
            var StudentViewModel = new StudentViewModel(myDataList);

            // A brute force way of sending a list for modifying student times
            SelectList list = new SelectList(time_list);
            ViewBag.myList = list;

            // Send to view
            return View(StudentViewModel);
        }

        /// <summary>
        /// View the class report page.
        /// </summary>
        /// <returns></returns>
        // GET: /Admin/ViewClassReport
        public ActionResult ViewClassReport()
        {
            return RedirectToAction("Index", "ClassReport");
        }

        /// <summary>
        /// View the student report page.
        /// </summary>
        /// <param name="id"></param> Student ID
        /// <returns></returns>
        // GET: /Admin/ViewClassReport
        public ActionResult ViewStudentReport(string id = null)
        {
            return RedirectToAction("StudentHistory", "RemoteStudent", new { id });
        }

        /// <summary>
        /// Find student and change their edit status to edit.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Admin/TriggerEdit
        public ActionResult TriggerEdit(string id = null)
        {
            if (id == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
            }

            // TODO: There has got to be a better way to do this. Leaving for now since
            // it's functional! :)
            // Query backend for student list
            var myDataList = StudentBackend.Index();
            var StudentViewModel = new StudentViewModel(myDataList);

            foreach (var item in StudentViewModel.StudentList)
            {
                if (id == item.Id)
                {
                    // We found the correct student. Toggle their archive status, then
                    // update the student in the backend server.
                    var updatedStudent = new StudentModel(item);
                    updatedStudent.IsEdit = true;
                    StudentBackend.Update(updatedStudent);
                } else if (item.IsEdit == true)
                {
                    // Force to false -- we do NOT support editing multiple students at once
                    var updatedStudent = new StudentModel(item);
                    updatedStudent.IsEdit = false;
                    StudentBackend.Update(updatedStudent);
                }
            }

            return RedirectToAction("Index");
        }

        // TODO: Future - Migrate to using Mike's Bind method(). Leaving as-is for demoability.
        // We will use the _official_ POST method on the student add page. Since this isn't a
        // coding class, Scott made a judgement call that this would be acceptable.
        //
        /// <summary>
        /// Find student and save changes made from the admin page. This is currently
        /// limited to adjusting attendance in/out times.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: Admin/Index
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            if (form == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
            }

            // Get student ID so we can find our student to update in the backend database
            string Id = form["Id"].ToString();

            // TODO: There has got to be a better way to do this. Leaving for now since
            // it's functional! :)
            // Query backend for student list
            var myDataList = StudentBackend.Index();
            var StudentViewModel = new StudentViewModel(myDataList);

            foreach (var item in StudentViewModel.StudentList)
            {
                if (Id == item.Id)
                {
                    // We found the correct student. Revert their edit status, then
                    // update the student in the backend server.
                    var updatedStudent = new StudentModel(item);
                    updatedStudent.TimeIn = form["TimeIn"].ToString();
                    updatedStudent.TimeOut = form["TimeOut"].ToString();
                    updatedStudent.IsEdit = false;

                    if (updatedStudent.TimeIn == "-----" && updatedStudent.TimeOut == "-----")
                    {
                        updatedStudent.DailyStatus = StudentDailyStatusEnum.Absent;
                    } else if (updatedStudent.TimeIn != "-----")
                    {
                        updatedStudent.DailyStatus = StudentDailyStatusEnum.Present;
                    }
                    else
                    {
                        // TODO: How to detect tardy students in the future?
                        updatedStudent.DailyStatus = StudentDailyStatusEnum.Absent;
                    }

                    StudentBackend.Update(updatedStudent);
                    break;
                }
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Find student and toggle their archival status.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Admin/ToggleArchive
        public ActionResult ToggleArchive(string id = null)
        {
            if (id == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
            }

            // TODO: There has got to be a better way to do this. Leaving for now since
            // it's functional! :)
            // Query backend for student list
            var myDataList = StudentBackend.Index();
            var StudentViewModel = new StudentViewModel(myDataList);

            foreach (var item in StudentViewModel.StudentList)
            {
                if(id == item.Id)
                {
                    // We found the correct student. Toggle their archive status, then
                    // update the student in the backend server.
                    var updatedStudent = new StudentModel(item);
                    updatedStudent.IsActive = (item.IsActive) ? false : true;
                    StudentBackend.Update(updatedStudent);
                    break;
                }
            }

            return RedirectToAction("Index");
        }

        // NOTE: This function contains The Endian's official POST method.
        /// <summary>
        /// Make a new Student sent in by the create Student screen
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: Student/Create
        [HttpPost]
        public ActionResult AddStudent([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "Username,"+
                                        "AvatarId,"+
                                        "IsActive,"+
                                        "IsEdit,"+
                                        "LoginStatus,"+
                                        "DailyStatus,"+
                                        "TimeIn,"+
                                        "TimeOut,"+
                                        "Password,"+
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
            StudentBackend.Create(data);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Redirects the user to the page where they can add a new student.
        /// </summary>
        /// <returns></returns>
        // GET: /Admin/AddStudent
        public ActionResult AddStudent()
        {
            var myData = new StudentModel();
            return View(myData);
        }

        /// <summary>
        /// Calls the data sources and has them reset to default data.
        /// </summary>
        /// <returns></returns>
        // GET: Reset
        public ActionResult Reset()
        {
            DataSourceBackend.Instance.Reset();
            return RedirectToAction("Index", "Admin");
        }

        // A brute force way of making a list of times. 
        // TODO: Clean up for production! Out of scope for 5051 class.
        public string[] time_list = new string[]
        {
            "-----", "7:00AM", "7:01AM", "7:02AM", "7:03AM", "7:04AM", "7:05AM", "7:06AM", "7:07AM", "7:08AM", "7:09AM", "7:10AM", "7:11AM", "7:12AM", "7:13AM", "7:14AM", "7:15AM", "7:16AM", "7:17AM", "7:18AM",
            "7:19AM", "7:20AM", "7:21AM", "7:22AM", "7:23AM", "7:24AM", "7:25AM", "7:26AM", "7:27AM", "7:28AM", "7:29AM", "7:30AM", "7:31AM", "7:32AM", "7:33AM", "7:34AM", "7:35AM", "7:36AM", "7:37AM",
            "7:38AM", "7:39AM", "7:40AM", "7:41AM", "7:42AM", "7:43AM", "7:44AM", "7:45AM", "7:46AM", "7:47AM", "7:48AM", "7:49AM", "7:50AM", "7:51AM", "7:52AM", "7:53AM", "7:54AM", "7:55AM", "7:56AM",
            "7:57AM", "7:58AM", "7:59AM", "8:00AM", "8:01AM", "8:02AM", "8:03AM", "8:04AM", "8:05AM", "8:06AM", "8:07AM", "8:08AM", "8:09AM", "8:10AM", "8:11AM", "8:12AM", "8:13AM", "8:14AM", "8:15AM",
            "8:16AM", "8:17AM", "8:18AM", "8:19AM", "8:20AM", "8:21AM", "8:22AM", "8:23AM", "8:24AM", "8:25AM", "8:26AM", "8:27AM", "8:28AM", "8:29AM", "8:30AM", "8:31AM", "8:32AM", "8:33AM", "8:34AM",
            "8:35AM", "8:36AM", "8:37AM", "8:38AM", "8:39AM", "8:40AM", "8:41AM", "8:42AM", "8:43AM", "8:44AM", "8:45AM", "8:46AM", "8:47AM", "8:48AM", "8:49AM", "8:50AM", "8:51AM", "8:52AM", "8:53AM",
            "8:54AM", "8:55AM", "8:56AM", "8:57AM", "8:58AM", "8:59AM", "9:00AM", "9:01AM", "9:02AM", "9:03AM", "9:04AM", "9:05AM", "9:06AM", "9:07AM", "9:08AM", "9:09AM", "9:10AM", "9:11AM", "9:12AM",
            "9:13AM", "9:14AM", "9:15AM", "9:16AM", "9:17AM", "9:18AM", "9:19AM", "9:20AM", "9:21AM", "9:22AM", "9:23AM", "9:24AM", "9:25AM", "9:26AM", "9:27AM", "9:28AM", "9:29AM", "9:30AM", "9:31AM",
            "9:32AM", "9:33AM", "9:34AM", "9:35AM", "9:36AM", "9:37AM", "9:38AM", "9:39AM", "9:40AM", "9:41AM", "9:42AM", "9:43AM", "9:44AM", "9:45AM", "9:46AM", "9:47AM", "9:48AM", "9:49AM", "9:50AM",
            "9:51AM", "9:52AM", "9:53AM", "9:54AM", "9:55AM", "9:56AM", "9:57AM", "9:58AM", "9:59AM", "10:00AM", "10:01AM", "10:02AM", "10:03AM", "10:04AM", "10:05AM", "10:06AM", "10:07AM", "10:08AM",
            "10:09AM", "10:10AM", "10:11AM", "10:12AM", "10:13AM", "10:14AM", "10:15AM", "10:16AM", "10:17AM", "10:18AM", "10:19AM", "10:20AM", "10:21AM", "10:22AM", "10:23AM", "10:24AM", "10:25AM",
            "10:26AM", "10:27AM", "10:28AM", "10:29AM", "10:30AM", "10:31AM", "10:32AM", "10:33AM", "10:34AM", "10:35AM", "10:36AM", "10:37AM", "10:38AM", "10:39AM", "10:40AM", "10:41AM", "10:42AM",
            "10:43AM", "10:44AM", "10:45AM", "10:46AM", "10:47AM", "10:48AM", "10:49AM", "10:50AM", "10:51AM", "10:52AM", "10:53AM", "10:54AM", "10:55AM", "10:56AM", "10:57AM", "10:58AM", "10:59AM",
            "11:00AM", "11:01AM", "11:02AM", "11:03AM", "11:04AM", "11:05AM", "11:06AM", "11:07AM", "11:08AM", "11:09AM", "11:10AM", "11:11AM", "11:12AM", "11:13AM", "11:14AM", "11:15AM", "11:16AM",
            "11:17AM", "11:18AM", "11:19AM", "11:20AM", "11:21AM", "11:22AM", "11:23AM", "11:24AM", "11:25AM", "11:26AM", "11:27AM", "11:28AM", "11:29AM", "11:30AM", "11:31AM", "11:32AM", "11:33AM",
            "11:34AM", "11:35AM", "11:36AM", "11:37AM", "11:38AM", "11:39AM", "11:40AM", "11:41AM", "11:42AM", "11:43AM", "11:44AM", "11:45AM", "11:46AM", "11:47AM", "11:48AM", "11:49AM", "11:50AM",
            "11:51AM", "11:52AM", "11:53AM", "11:54AM", "11:55AM", "11:56AM", "11:57AM", "11:58AM", "11:59AM", "12:00PM", "12:01PM", "12:02PM", "12:03PM", "12:04PM", "12:05PM", "12:06PM", "12:07PM",
            "12:08PM", "12:09PM", "12:10PM", "12:11PM", "12:12PM", "12:13PM", "12:14PM", "12:15PM", "12:16PM", "12:17PM", "12:18PM", "12:19PM", "12:20PM", "12:21PM", "12:22PM", "12:23PM", "12:24PM",
            "12:25PM", "12:26PM", "12:27PM", "12:28PM", "12:29PM", "12:30PM", "12:31PM", "12:32PM", "12:33PM", "12:34PM", "12:35PM", "12:36PM", "12:37PM", "12:38PM", "12:39PM", "12:40PM", "12:41PM",
            "12:42PM", "12:43PM", "12:44PM", "12:45PM", "12:46PM", "12:47PM", "12:48PM", "12:49PM", "12:50PM", "12:51PM", "12:52PM", "12:53PM", "12:54PM", "12:55PM", "12:56PM", "12:57PM", "12:58PM",
            "12:59PM", "1:00PM", "1:01PM", "1:02PM", "1:03PM", "1:04PM", "1:05PM", "1:06PM", "1:07PM", "1:08PM", "1:09PM", "1:10PM", "1:11PM", "1:12PM", "1:13PM", "1:14PM", "1:15PM", "1:16PM", "1:17PM",
            "1:18PM", "1:19PM", "1:20PM", "1:21PM", "1:22PM", "1:23PM", "1:24PM", "1:25PM", "1:26PM", "1:27PM", "1:28PM", "1:29PM", "1:30PM", "1:31PM", "1:32PM", "1:33PM", "1:34PM", "1:35PM", "1:36PM",
            "1:37PM", "1:38PM", "1:39PM", "1:40PM", "1:41PM", "1:42PM", "1:43PM", "1:44PM", "1:45PM", "1:46PM", "1:47PM", "1:48PM", "1:49PM", "1:50PM", "1:51PM", "1:52PM", "1:53PM", "1:54PM", "1:55PM",
            "1:56PM", "1:57PM", "1:58PM", "1:59PM", "2:00PM", "2:01PM", "2:02PM", "2:03PM", "2:04PM", "2:05PM", "2:06PM", "2:07PM", "2:08PM", "2:09PM", "2:10PM", "2:11PM", "2:12PM", "2:13PM", "2:14PM",
            "2:15PM", "2:16PM", "2:17PM", "2:18PM", "2:19PM", "2:20PM", "2:21PM", "2:22PM", "2:23PM", "2:24PM", "2:25PM", "2:26PM", "2:27PM", "2:28PM", "2:29PM", "2:30PM", "2:31PM", "2:32PM", "2:33PM",
            "2:34PM", "2:35PM", "2:36PM", "2:37PM", "2:38PM", "2:39PM", "2:40PM", "2:41PM", "2:42PM", "2:43PM", "2:44PM", "2:45PM", "2:46PM", "2:47PM", "2:48PM", "2:49PM", "2:50PM", "2:51PM", "2:52PM",
            "2:53PM", "2:54PM", "2:55PM", "2:56PM", "2:57PM", "2:58PM", "2:59PM", "3:00PM", "3:01PM", "3:02PM", "3:03PM", "3:04PM", "3:05PM", "3:06PM", "3:07PM", "3:08PM", "3:09PM", "3:10PM", "3:11PM",
            "3:12PM", "3:13PM", "3:14PM", "3:15PM", "3:16PM", "3:17PM", "3:18PM", "3:19PM", "3:20PM", "3:21PM", "3:22PM", "3:23PM", "3:24PM", "3:25PM", "3:26PM", "3:27PM", "3:28PM", "3:29PM", "3:30PM",
            "3:31PM", "3:32PM", "3:33PM", "3:34PM", "3:35PM", "3:36PM", "3:37PM", "3:38PM", "3:39PM", "3:40PM", "3:41PM", "3:42PM", "3:43PM", "3:44PM", "3:45PM", "3:46PM", "3:47PM", "3:48PM", "3:49PM",
            "3:50PM", "3:51PM", "3:52PM", "3:53PM", "3:54PM", "3:55PM", "3:56PM", "3:57PM", "3:58PM", "3:59PM"
        };
    }
}