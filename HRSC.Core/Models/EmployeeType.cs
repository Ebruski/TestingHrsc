using HRSC.Core.Enums;

namespace HRSC.Core.Models
{
    public class EmployeeType : BaseModel
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public PaymentType PaymentType { get; set; }
        public SalaryType SalaryType { get; set; }
        public bool HasTax { get; set; }
        public decimal Amount { get; set; }
    }
}
