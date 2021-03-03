using HRSC.Core.Models;
using System.Collections.Generic;

namespace HRSC.Web.Models
{
    public class StaticCache
    {
        public static User User { get; set; }
        public static decimal Tax { get; set; }
        public static IEnumerable<EmployeeType> EmployeeTypes { get; set; }
        public static IEnumerable<Employee> Employees { get; set; }
    }
}
