using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Models;

namespace _5051.Backend
{

    /// <summary>
    /// Datasource Interface for StudentCheckins
    /// </summary>
    public interface IStudentCheckinInterface
    {
        StudentCheckinModel Create(StudentCheckinModel data);
        StudentCheckinModel Read(string id);
        StudentCheckinModel Update(StudentCheckinModel data);
        bool Delete(string id);
        List<StudentCheckinModel> Index();
        void Reset();
    }
}