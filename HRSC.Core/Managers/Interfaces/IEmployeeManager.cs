using HRSC.Core.DTO;
using HRSC.Core.Extensions;
using HRSC.Core.Models;
using System;
using System.Collections.Generic;

namespace HRSC.Core.Managers.Interfaces
{
    public interface IEmployeeManager
    {
        Result<Employee> Get(Guid employeeId);
        IEnumerable<Employee> GetAll();
        Result<Guid> Create(EmployeeDTO employee);
        Result<bool> Delete(Guid employeeId);
        Result<Employee> Update(EmployeeDTO employee);
        Result<ComputeSalaryResponse> CalculateSalary(ComputeSalaryRequest model);
    }
}
