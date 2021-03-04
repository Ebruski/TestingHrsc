using HRSC.Core.DTO;
using HRSC.Core.Extensions;
using HRSC.Core.Models;

namespace HRSC.Core.Adapters.Interfaces
{
    public interface ISalaryCalculator
    {
        Result<ComputeSalaryResponse> ComputeForRegularStaff(EmployeeType empType, double daysAbsent, string month);
        Result<ComputeSalaryResponse> ComputeForContractualStaff(EmployeeType empType, double daysWorked, string month);
    }
}
