using System;
using System.Collections.Generic;

using _5051.Models;

namespace _5051.Backend
{
    /// <summary>
    /// Holds the Student Data as a Mock Data set, used for Unit Testing, System Testing, Offline Development etc.
    /// </summary>
    public class StudentDataSourceMock : IStudentInterface
    {
        // Gain access to system-wide globals. May be a better way to do this.
        private SystemGlobals var_g = SystemGlobals.Instance;

        /// <summary>
        /// Make into a singleton
        /// </summary>
        private static volatile StudentDataSourceMock instance;
        private static object syncRoot = new Object();

        private StudentDataSourceMock() { }

        public static StudentDataSourceMock Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new StudentDataSourceMock();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// The Data for the Students
        /// </summary>
        private List<StudentModel> StudentList = new List<StudentModel>();

        /// <summary>
        /// Makes a new Student
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Student Passed In</returns>
        public StudentModel Create(StudentModel data)
        {
            StudentList.Add(data);
            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public StudentModel Read(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myReturn = StudentList.Find(n => n.Id == id);
            return myReturn;
        }

        /// <summary>
        /// Update all attributes to be what is passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Null or updated data</returns>
        public StudentModel Update(StudentModel data)
        {
            if (data == null)
            {
                return null;
            }
            var myReturn = StudentList.Find(n => n.Id == data.Id);

            myReturn.Update(data);

            return myReturn;
        }

        /// <summary>
        /// Remove the Data item by ID if it is in the list
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True for success, else false</returns>
        public bool Delete(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return false;
            }

            var myData = StudentList.Find(n => n.Id == Id);
            var myReturn = StudentList.Remove(myData);
            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of Students</returns>
        public List<StudentModel> Index()
        {
            return StudentList;
        }

        /// <summary>
        /// Reset the Data, and reload it
        /// </summary>
        public void Reset()
        {
            Initialize();
        }

        /// <summary>
        /// Create Placeholder Initial Data
        /// </summary>
        public void Initialize()
        {
            LoadDataSet(DataSourceDataSetEnum.Default);
        }

        /// <summary>
        /// Clears the Data
        /// </summary>
        private void DataSetClear()
        {
            StudentList.Clear();
        }

        /// <summary>
        /// The Defalt Data Set
        /// </summary>
        private void DataSetDefault()
        {
            // Using this dummy student purely so we can access avatarUri below. Again,
            // there's got a be a cleaner way to do this in C#.
            var dummy = new StudentModel();

            DataSetClear();

            // Bogus example data
            Create(new StudentModel("Kevin Lundeen", "Agud Boi", dummy.avatarUri[9], StudentLoginStatusEnum.Out, StudentDailyStatusEnum.Absent, var_g.defaultTime, var_g.defaultTime));
            Create(new StudentModel("Sheila Oh", "Koala24", dummy.avatarUri[3], StudentLoginStatusEnum.In, StudentDailyStatusEnum.Present, "8:34AM", var_g.defaultTime));
            Create(new StudentModel("Lin Li", "Butterfly123", dummy.avatarUri[4], StudentLoginStatusEnum.In, StudentDailyStatusEnum.Present, "7:47AM", var_g.defaultTime));
            Create(new StudentModel("Andrew Croneberger", "CronenBucks", dummy.avatarUri[5], StudentLoginStatusEnum.In, StudentDailyStatusEnum.Tardy, "9:23AM", var_g.defaultTime));
            Create(new StudentModel("Pejman Khadivi", "Dr. Mutex", dummy.avatarUri[1], StudentLoginStatusEnum.In, StudentDailyStatusEnum.Tardy, "9:10AM", var_g.defaultTime));
            Create(new StudentModel("Mike Koenig", "mikey44", dummy.avatarUri[0], StudentLoginStatusEnum.Out, StudentDailyStatusEnum.Present, "8:05AM", "2:45PM"));
            Create(new StudentModel("James Obare", "Waldo", dummy.avatarUri[10], StudentLoginStatusEnum.Out, StudentDailyStatusEnum.Absent, var_g.defaultTime, var_g.defaultTime));
        }

        /// <summary>
        /// Data set for demo
        /// </summary>
        private void DataSetDemo()
        {
            DataSetDefault();
        }

        /// <summary>
        /// Unit Test data set
        /// </summary>
        private void DataSetUnitTest()
        {
            DataSetDefault();
        }

        /// <summary>
        /// Set which data to load
        /// </summary>
        /// <param name="setEnum"></param>
        public void LoadDataSet(DataSourceDataSetEnum setEnum)
        {
            switch (setEnum)
            {
                case DataSourceDataSetEnum.Demo:
                    DataSetDemo();
                    break;

                case DataSourceDataSetEnum.UnitTest:
                    DataSetUnitTest();
                    break;

                case DataSourceDataSetEnum.Default:
                default:
                    DataSetDefault();
                    break;
            }
        }
    }
}