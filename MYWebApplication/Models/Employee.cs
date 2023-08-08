using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYWebApplication.Models
{
    public class Employee
    {
     
        public int EmpID { get; set; }
        public int Password { get; set; }
        public string UserName { get; set; }
        public string ProjectName { get; set; }

        public string ProjectManagerName { get; set; }

        public string EmpDesignation { get; set; }
    }
}
