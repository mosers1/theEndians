using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace _5051.Models
{
    /// <summary>
    /// Student Checkins for the system
    /// </summary>
    public class StudentCheckinModel
    {
        [Display(Name = "Id", Description = "Avatar Id")]
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; }

        [Display(Name = "Uri", Description = "Picture to Show")]
        [Required(ErrorMessage = "Picture is required")]
        public string Uri { get; set; }

        [Display(Name = "Name", Description = "Avatar Name")]
        [Required(ErrorMessage = "Avatar Name is required")]
        public string Name { get; set; }

        [Display(Name = "CheckedIn", Description = "CheckedIn")]
        [Required(ErrorMessage = "Student Checkin is required")]
        public Boolean CheckedIn { get; set; }

        /// <summary>
        /// Create the default values
        /// </summary>
        public void Initialize()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// New StudentCheckinModel
        /// </summary>
        public StudentCheckinModel()
        {
            Initialize();
        }

        /// <summary>
        /// Make a StudentCheckinModel from values passed in
        /// </summary>
        /// <param name="uri">The Picture path</param>
        /// <param name="name">Avatar Name</param>
        /// <param name="description">Avatar Description</param>
        public StudentCheckinModel(string uri, string name, bool checkedIn)
        {
            Initialize();

            Uri = uri;
            Name = name;
            CheckedIn = checkedIn;
        }
    }
}