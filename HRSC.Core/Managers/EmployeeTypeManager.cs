using HRSC.Core.Data;
using HRSC.Core.Extensions;
using HRSC.Core.Managers.Interfaces;
using HRSC.Core.Models;
using System;
using System.Collections.Generic;

namespace HRSC.Core.Managers
{
    public class EmployeeTypeManager : IEmployeeTypeManager
    {
        private readonly DataContext<User> _userContext;
        private readonly DataContext<Employee> _employeeContext;
        private readonly DataContext<EmployeeType> _employeeTypeContext;

        public EmployeeTypeManager()
        {
            _employeeContext = new DataContext<Employee>();
            _employeeTypeContext = new DataContext<EmployeeType>();
            _userContext = new DataContext<User>();
        }

        public Result<Guid> Create(EmployeeType employeeType)
        {
            throw new NotImplementedException();
        }

        public Result<bool> Delete(Guid employeeTypeId)
        {
            throw new NotImplementedException();
        }

        public Result<EmployeeType> Get(Guid employeeTypeId)
        {
            if (employeeTypeId == Guid.Empty) return Result<EmployeeType>.Failure("EmployeeId is required");

            var empType = _employeeTypeContext.GetById(employeeTypeId);

            if (empType == null)
                return Result<EmployeeType>.Failure($"Sorry, employee type  not found");

            return Result<EmployeeType>.Success(empType, "Employee type found");
        }

        public IEnumerable<EmployeeType> GetAll()
        {
            return _employeeTypeContext.GetAll();
        }

        public Result<EmployeeType> Update(Guid employeeTypeId)
        {
            throw new NotImplementedException();
        }
    }
}
