using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5051.Models
{
    /// <summary>
    /// System wide Global variables
    /// </summary>
    public class SystemGlobals
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile SystemGlobals instance;
        private static object syncRoot = new Object();

        private SystemGlobals() { }

        public static SystemGlobals Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SystemGlobals();
                        }
                    }
                }

                return instance;
            }
        }

        // The Enum to use for the current data source
        // Default to Mock
        public DataSourceEnum DataSourceValue = DataSourceEnum.Mock;

        public string defaultTime = "-----";
    }
}