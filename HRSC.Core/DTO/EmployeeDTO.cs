using System;

namespace HRSC.Core.DTO
{
    public class EmployeeDTO
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string TIN { get; set; }
        public Guid EmployeeTypeId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
