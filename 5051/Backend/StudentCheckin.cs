using _5051.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _5051.Backend
{
    /// <summary>
    /// Backend handles the business logic and data for checkin
    /// </summary>
    public class StudentCheckinBackend
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile StudentCheckinBackend instance;
        private static object syncRoot = new Object();

        private StudentCheckinBackend() { }

        public static StudentCheckinBackend Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new StudentCheckinBackend();
                            SetDataSource();
                        }
                    }
                }

                return instance;
            }
        }

        // Get the Datasource to use
        private static IStudentCheckinInterface DataSource;

        /// <summary>
        /// Sets the Datasource to be Mock or SQL
        /// </summary>
        /// <param name="dataSourceEnum"></param>
        public static void SetDataSource()
        {
            // Default is to use the Mock
            DataSource = StudentCheckinDataSourceMock.Instance;
        }

        /// <summary>
        /// Makes a new StudentCheckinn
        /// </summary>
        /// <param name="data"></param>
        /// <returns>StudentCheckinn Passed In</returns>
        public StudentCheckinModel Create(StudentCheckinModel data)
        {
            DataSource.Create(data);
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

            var myReturn = DataSource.Read(id);
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

            var myReturn = DataSource.Update(data);

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

            var myReturn = DataSource.Delete(Id);
            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of StudentCheckinns</returns>
        public List<StudentCheckinModel> Index()
        {
            var myData = DataSource.Index();
            return myData;
        }

        /// <summary>
        /// Helper that returns the First StudentCheckinn ID in the list, this will be used for creating new StudentCheckinns if no StudentCheckinnID is specified
        /// </summary>
        /// <returns>Null, or StudentCheckinn ID of the first StudentCheckinn in the list.</returns>
        public string GetFirstStudentCheckinnId()
        {
            string myReturn = null;

            var myData = DataSource.Index().ToList().FirstOrDefault();
            if (myData != null)
            {
                myReturn = myData.Id;
            }

            return myReturn;
        }

        /// <summary>
        /// Helper function that returns the StudentCheckinn Image URI
        /// </summary>
        /// <param name="data">The StudentCheckinnId to look up</param>
        /// <returns>null, or the StudentCheckinn image URI</returns>
        public string GetStudentCheckinnUri(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            string myReturn = null;

            var myData = DataSource.Read(data);
            if (myData != null)
            {
                myReturn = myData.Uri;
            }

            return myReturn;
        }

        /// <summary>
        /// Helper that gets the list of Items, and converst them to a SelectList, so they can show in a Drop Down List box
        /// </summary>
        /// <param name="id">optional paramater, of the Item that is currently selected</param>
        /// <returns>List of SelectListItems as a SelectList</returns>
        public List<SelectListItem> GetStudentCheckinnListItem(string id = null)
        {
            var myDataList = DataSource.Index();

            //var myReturn = new SelectList(myDataList);

            var myReturn = myDataList.Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id,
                Selected = (a.Id == id),
            }).ToList();

            return myReturn;
        }

        /// <summary>
        /// Helper function that resets the DataSource, and rereads it.
        /// </summary>
        public void Reset()
        {
            DataSource.Reset();
        }
    }
}