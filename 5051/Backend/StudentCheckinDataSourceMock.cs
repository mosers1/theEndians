using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using _5051.Models;
namespace _5051.Backend
{
    /// <summary>
    /// Backend Mock DataSource for Avatars, to manage them
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
        /// The Avatar List
        /// </summary>
        private List<StudentCheckinModel> avatarList = new List<StudentCheckinModel>();

        /// <summary>
        /// Makes a new Avatar
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Avatar Passed In</returns>
        public StudentCheckinModel Create(StudentCheckinModel data)
        {
            avatarList.Add(data);
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

            var myReturn = avatarList.Find(n => n.Id == id);
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
            var myReturn = avatarList.Find(n => n.Id == data.Id);
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

            var myData = avatarList.Find(n => n.Id == Id);
            var myReturn = avatarList.Remove(myData);
            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of Avatars</returns>
        public List<StudentCheckinModel> Index()
        {
            return avatarList;
        }

        /// <summary>
        /// Reset the Data, and reload it
        /// </summary>
        public void Reset()
        {
            avatarList.Clear();
            Initialize();
        }

        /// <summary>
        /// Create Placeholder Initial Data
        /// </summary>
        public void Initialize()
        {
            Create(new StudentCheckinModel("person1.png", "Kevin Cushing", true));
            Create(new StudentCheckinModel("person2.png", "Maggie", false));
            Create(new StudentCheckinModel("person3.png", "Scott", false));
            Create(new StudentCheckinModel("person4.png", "Andrew C", true));
            Create(new StudentCheckinModel("person5.png", "Andrew W", true));
            Create(new StudentCheckinModel("person6.png", "Steve", false));
        }
    }
}