using HRSC.Core.Extensions;
using HRSC.Core.Models;
using System;
using System.Collections.Generic;

namespace HRSC.Core.Managers.Interfaces
{
    public interface IEmployeeTypeManager
    {
        Result<EmployeeType> Get(Guid employeeTypeId);
        IEnumerable<EmployeeType> GetAll();
        Result<Guid> Create(EmployeeType employeeType);
        Result<bool> Delete(Guid employeeTypeId);
        Result<EmployeeType> Update(Guid employeeTypeId);
    }
}
