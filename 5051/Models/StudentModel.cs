using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using _5051.Backend;

namespace _5051.Models
{
    /// <summary>
    /// The Student and related information. At present, this class contains all information related
    /// to a student: name, ID, attendance status, daily sign-in/out time, avatar, etc.
    /// TODO: Future - Consider moving parts of data around to other models/modelviews for a cleaner
    /// design.
    /// </summary>
    public class StudentModel
    {
        // Gain access to system-wide globals. May be a better way to do this.
        private SystemGlobals var_g = SystemGlobals.Instance;

        /// <summary>
        /// The ID for the Student, this is the key, and a required field
        /// </summary>
        [Key]
        [Display(Name = "ID", Description = "Student ID")]
        [Required(ErrorMessage = "Student ID is required")]
        public string Id { get; set; }

        /// <summary>
        /// The Friendly name for the student, does _not_ need to be directly associated with the actual student name
        /// </summary>
        [Display(Name = "Name", Description = "Student Name")]
        [Required(ErrorMessage = "Student name is required")]
        public string Name { get; set; }

        /// <summary>
        /// The student's username.
        /// </summary>
        [Display(Name = "Username", Description = "Student Username")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        /// <summary>
        /// The ID of the Avatar the student is associated with, this will convert to an avatar picture
        /// </summary>
        [Display(Name = "AvatarId", Description = "Avatar")]
        public string AvatarId { get; set; }

        /// <summary>
        /// The student archival status. If they leave mid-year, they can be archived. If they return,
        /// they can be brought back.
        /// </summary>
        [Display(Name = "IsActive", Description = "Is student active or archived?")]
        public bool IsActive { get; set; }

        /// <summary>
        /// The edit-in-progress status set/cleared by the admin used when modifying attendance records.
        /// </summary>
        [Display(Name = "IsEdit", Description = "Is the student being edited?")]
        public bool IsEdit { get; set; }

        /// <summary>
        /// The status of the student, for example currently logged in, out
        /// </summary>
        [Display(Name = "Current Login Status", Description = "Login status of the Student")]
        public StudentLoginStatusEnum LoginStatus { get; set; }

        // TODO: Move to another model? Leaving here for demo.
        /// <summary>
        /// The daily status of the student, for example currently logged in, out
        /// </summary>
        [Display(Name = "Daily Status", Description = "Daily status of the Student")]
        public StudentDailyStatusEnum DailyStatus { get; set; }

        // TODO: Move to another model? Leaving here for demo.
        /// <summary>
        /// The daily sign-in time of the student.
        /// </summary>
        [Display(Name = "Time In", Description = "Student Sign-in Time")]
        public string TimeIn { get; set; }

        // TODO: Move to another model? Leaving here for demo.
        /// <summary>
        /// The daily sign-out time of the student.
        /// </summary>
        [Display(Name = "Time Out", Description = "Student Sign-out Time")]
        public string TimeOut { get; set; }

        /// <summary>
        /// The student account's password.
        /// </summary>
        [Display(Name = "Password", Description = "Student Password")]
        [PasswordPropertyText]
        public string Password { get; set; }

        /// <summary>
        /// Initialize the default values for a new student.
        /// </summary>
        public void Initialize()
        {
            Id = Guid.NewGuid().ToString();
            Name = string.Empty;
            LoginStatus = StudentLoginStatusEnum.Out;
            DailyStatus = StudentDailyStatusEnum.Absent;
            Username = Name;             // TODO: Better idea here? Default to name for now.
            AvatarId = avatarUri[avatarUri.Length - 1];  // Default to last avatar icon in list
            IsActive = true;
            IsEdit = false;
            TimeIn = var_g.defaultTime;     
            TimeOut = var_g.defaultTime; 
            Password = string.Empty;
        }

        /// <summary>
        /// Constructor for a student
        /// </summary>
        public StudentModel()
        {
            Initialize();
        }

        /// <summary>
        /// Overloaded constructor for Student. Call this when making a new student.
        /// </summary>
        /// <param name="_name">Student name</param>
        /// <param name="_username">Student username</param>
        /// <param name="_avatarId">The avatar to use</param>
        /// <param name="_loginStatus">Student current log-in status</param>
        /// <param name="_dailyStatus">Student current daily status</param>
        /// <param name="_timeIn">Student sign-in time</param>
        /// <param name="_timeOut">Student sign-out time</param>
        public StudentModel(string _name, string _username, string _avatarId, StudentLoginStatusEnum _loginStatus,
            StudentDailyStatusEnum _dailyStatus, string _timeIn, string _timeOut)
        {
            Initialize();

            Name = _name;
            Username = _username;
            AvatarId = _avatarId;
            LoginStatus = _loginStatus;
            DailyStatus = _dailyStatus;
            TimeIn = _timeIn;
            TimeOut = _timeOut;
        }

        /// <summary>
        /// Convert a Student Display View Model, to a Student Model, used for when passed data from Views that use the full Student Display View Model.
        /// </summary>
        /// <param name="data">The student data to pull</param>
        public StudentModel(StudentModel data)
        {
            this.Id = data.Id;
            this.Name = data.Name;
            this.AvatarId = data.AvatarId;
            this.Username = data.Username;
            this.IsActive = data.IsActive;
            this.IsEdit = data.IsEdit;
            this.LoginStatus = data.LoginStatus;
            this.DailyStatus = data.DailyStatus;
            this.TimeIn = data.TimeIn;
            this.TimeOut = data.TimeOut;
            this.Password = data.Password;
        }

        /// <summary>
        /// Update the Data Fields with the values passed in, do not update the ID.
        /// </summary>
        /// <param name="data">The values to update</param>
        /// <returns>False if null, else true</returns>
        public bool Update(StudentModel data)
        {
            if (data == null)
            {
                return false;
            }

            Name = data.Name;
            AvatarId = data.AvatarId;
            IsActive = data.IsActive;
            IsEdit = data.IsEdit;
            Username = data.Username;
            LoginStatus = data.LoginStatus;
            DailyStatus = data.DailyStatus;
            TimeIn = data.TimeIn;
            TimeOut = data.TimeOut;
            Password = data.Password;

            return true;
        }

        // Holds the list of avatarURI for this website
        public string[] avatarUri = new string[]
        {
            "person1.png", "person2.png", "person3.png",
            "person4.png", "person5.png", "person6.png",
            "person7.png", "person8.png", "person9.png",
            "person10.png", "person11.png", "person12.png"
        };
    }

    /// <summary>
    /// Student Login Status Options
    /// </summary>
    public enum StudentLoginStatusEnum
    {
        // Unknown status
        Unknown = 0,

        // Logged Out
        Out = 1,

        // Logged In
        In = 2
    }

    /// <summary>
    /// Student Daily Status Options
    /// </summary>
    public enum StudentDailyStatusEnum
    {
        // Unknown
        Unknown = 0,
        
        // Present - full day
        Present = 1,

        // Late or left early
        Tardy = 2,

        // Absent all day
        Absent = 3
    }
}
