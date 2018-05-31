using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using _5051.Models;
namespace _5051.Backend
{
    /// <summary>
    /// Backend Mock DataSource for StudentCheckins, to manage them
    /// </summary>
    public class StudentCheckinDataSourceMock : IStudentCheckinInterface
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile StudentCheckinDataSourceMock instance;
        private static object syncRoot = new Object();

        private StudentCheckinDataSourceMock() { }

        public static StudentCheckinDataSourceMock Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new StudentCheckinDataSourceMock();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// The StudentCheckin List
        /// </summary>
        private List<StudentCheckinModel> StudentCheckinList = new List<StudentCheckinModel>();

        /// <summary>
        /// Makes a new StudentCheckin
        /// </summary>
        /// <param name="data"></param>
        /// <returns>StudentCheckin Passed In</returns>
        public StudentCheckinModel Create(StudentCheckinModel data)
        {
            StudentCheckinList.Add(data);
            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public StudentCheckinModel Read(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myReturn = StudentCheckinList.Find(n => n.Id == id);
            return myReturn;
        }

        /// <summary>
        /// Update all attributes to be what is passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Null or updated data</returns>
        public StudentCheckinModel Update(StudentCheckinModel data)
        {
            if (data == null)
            {
                return null;
            }
            var myReturn = StudentCheckinList.Find(n => n.Id == data.Id);
            myReturn.Name = data.Name;
            myReturn.CheckedIn = data.CheckedIn;
            myReturn.Uri = data.Uri;

            return myReturn;
        }

        /// <summary>
        /// Remove the Data item if it is in the list
        /// </summary>
        /// <param name="data"></param>
        /// <returns>True for success, else false</returns>
        public bool Delete(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return false;
            }

            var myData = StudentCheckinList.Find(n => n.Id == Id);
            var myReturn = StudentCheckinList.Remove(myData);
            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of StudentCheckins</returns>
        public List<StudentCheckinModel> Index()
        {
            return StudentCheckinList;
        }

        /// <summary>
        /// Reset the Data, and reload it
        /// </summary>
        public void Reset()
        {
            StudentCheckinList.Clear();
            Initialize();
        }

        /// <summary>
        /// Create Placeholder Initial Data
        /// </summary>
        public void Initialize()
        {
            Create(new StudentCheckinModel("person1.png", "Kevin Cushing", true));
            Create(new StudentCheckinModel("person2.png", "Maggie Dong", false));
            Create(new StudentCheckinModel("person3.png", "Scott Moser", false));
            Create(new StudentCheckinModel("person4.png", "Andrew Croneberger", true));
            Create(new StudentCheckinModel("person5.png", "Andrew Wallace", true));
            Create(new StudentCheckinModel("person6.png", "Mike Koenig", false));
        }
    }
}