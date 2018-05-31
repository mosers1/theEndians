using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5051.Models
{
    /// <summary>
    /// View Model for the Student Index, this will have the list of the students in it convered to a StudentViewModel
    /// </summary>
    public class StudentViewModel
    {
        /// <summary>
        /// The student List to return to the View
        /// </summary>
        public List<StudentModel> StudentList = new List<StudentModel>();

        /// <summary>
        /// Default constructor, needed becase of the constructor that takes a List of Student Models
        /// </summary>
        public StudentViewModel() { }

        /// <summary>
        /// Take the data list passed in, and convert each to a new StudentDisplayViewModel item and add that to the StudentList
        /// </summary>
        /// <param name="dataList"></param>
        public StudentViewModel(List<StudentModel> dataList)
        {
            foreach (var item in dataList)
            {
                StudentList.Add(new StudentModel(item));
            }
        }
    }
}