using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYWebApplication.DataAccess
{
    public class DBaccess
    {
    }
        public class ConnectionString
        {
            private static string cName = "Data Source=(localdb)\\MSSqlLocalDb; Initial Catalog=EmployeeProject;Trusted_Connection=True";
            public static string CName
            {
                get => cName;
            }
        }
    }

