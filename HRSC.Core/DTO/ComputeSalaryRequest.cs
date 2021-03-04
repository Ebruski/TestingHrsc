using System;

namespace HRSC.Core.DTO
{
    public class ComputeSalaryRequest
    {
        public double DaysAbsent { get; set; }
        public double DaysWorked { get; set; }
        public string Month { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
