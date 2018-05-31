using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5051.Models
{
    /// <summary>
    /// The Mock Status
    /// </summary>
    public enum DataSourceEnum
    {
        // Not specified
        Unknown = 0,

        // Mock Dataset
        Mock = 1,

        // SQL Dataset
        SQL = 2
    }

    public enum DataSourceDataSetEnum
    {
        // Not specified
        Default = 0,

        // Mock Dataset
        Demo = 1,

        // SQL Dataset
        UnitTest = 2
    }
}