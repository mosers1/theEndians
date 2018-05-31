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
    public class StudentCheckinViewModel
    {
        public List<StudentCheckinModel> CheckinList = new List<StudentCheckinModel>();
    }
}