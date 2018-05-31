using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

// TODO: Implement in the future once a backend is required.
using _5051.Backend;

namespace _5051.Models
{
    /// <summary>
    /// The Student and related information. Other things about the student such
    /// as their attendance is pulled from the attendance data, or the owned 
    /// items, from the inventory data
    /// </summary>
    public class StudentModel
    {
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
        /// The student's self-chosen username
        /// </summary>
        [Display(Name = "Username", Description = "Student Username")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        /// <summary>
        /// The ID of the Avatar the student is associated with, this will convert to an avatar picture
        /// </summary>
        [Display(Name = "AvatarId", Description = "Avatar")]
        [Required(ErrorMessage = "Avatar is required")]
        public string AvatarId { get; set; }

        /// <summary>
        /// The personal level for the Avatar, the avatar levels up.  switching the avatar ID (picture), does not change the level
        /// </summary>
        [Display(Name = "IsActive", Description = "Is student active or archived?")]
        [Required(ErrorMessage = "Student status is required")]
        public bool IsActive { get; set; }

        /// <summary>
        /// The edit-in-progress status set/cleared by the admin.
        /// </summary>
        [Display(Name = "IsEdit", Description = "Is the student being edited?")]
        [Required(ErrorMessage = "Edit status is required")]
        public bool IsEdit { get; set; }

        /// <summary>
        /// The status of the student, for example currently logged in, out
        /// </summary>
        [Display(Name = "Current Login Status", Description = "Login status of the Student")]
        [Required(ErrorMessage = "Login status is required")]
        public StudentLoginStatusEnum LoginStatus { get; set; }

        // TODO: Move to another model? Leaving here for demo.
        /// <summary>
        /// The daily status of the student, for example currently logged in, out
        /// </summary>
        [Display(Name = "Daily Status", Description = "Daily status of the Student")]
        [Required(ErrorMessage = "Daily status is required")]
        public StudentDailyStatusEnum DailyStatus { get; set; }

        // TODO: Move to another model? Leaving here for demo.
        /// <summary>
        /// The daily sign-in time of the student.
        /// </summary>
        [Display(Name = "Time In", Description = "Student Sign-in Time")]
        [Required(ErrorMessage = "Sign-in time is required")]
        public string TimeIn { get; set; }

        // TODO: Move to another model? Leaving here for demo.
        /// <summary>
        /// The daily sign-out time of the student.
        /// </summary>
        [Display(Name = "Time Out", Description = "Student Sign-out Time")]
        [Required(ErrorMessage = "Sign-out time is required")]
        public string TimeOut { get; set; }

        /// <summary>
        /// The student account's password
        /// </summary>
        [Display(Name = "Password", Description = "Student Password")]
        [PasswordPropertyText]
        public string Password { get; set; }

        /// <summary>
        /// The defaults for a new student
        /// </summary>
        public void Initialize()
        {
            Id = Guid.NewGuid().ToString();
            Name = string.Empty;
            LoginStatus = StudentLoginStatusEnum.Out;
            DailyStatus = StudentDailyStatusEnum.Absent;
            Username = Name;             // TODO: Better idea here? Leave for now.
            AvatarId = string.Empty;     // TODO: Enum instead?
            IsActive = true;
            IsEdit = false;
            TimeIn = "-----";     
            TimeOut = "-----"; 
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
        /// Constructor for Student.  Call this when making a new student
        /// </summary>
        /// <param name="name">The Name to call the student</param>
        /// <param name="avatarId">The avatar to use
        public StudentModel(string name, string avatarId)
        {
            Initialize();

            Name = name;
            AvatarId = avatarId;
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
        
        // Present - fyull day
        Present = 1,

        // Late or left earl
        Tardy = 2,

        // Absent all day
        Absent = 3
    }
}
