using System;

namespace HRSC.Core.Models
{
    public class Employee : BaseModel
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string TIN { get; set; }

        public EmployeeType EmployeeType { get; set; }
    }
}
