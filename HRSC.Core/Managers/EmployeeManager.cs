using HRSC.Core.Adapters.Interfaces;
using HRSC.Core.Data;
using HRSC.Core.DTO;
using HRSC.Core.Enums;
using HRSC.Core.Extensions;
using HRSC.Core.Managers.Interfaces;
using HRSC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HRSC.Core.Managers
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly DataContext<Employee> _employeeContext;
        private readonly DataContext<EmployeeType> _employeeTypeContext;

        private readonly ISalaryCalculator _salaryCalculator;
        private readonly IPaymentManager _paymentManager;

        public EmployeeManager(ISalaryCalculator salaryCalculator, IPaymentManager paymentManager)
        {
            _employeeContext = new DataContext<Employee>();
            _employeeTypeContext = new DataContext<EmployeeType>();

            _paymentManager = paymentManager;
            _salaryCalculator = salaryCalculator;
        }

        public Result<ComputeSalaryResponse> CalculateSalary(ComputeSalaryRequest model)
        {
            if (string.IsNullOrEmpty(model.Month)) return Result<ComputeSalaryResponse>.Failure("Month is required");
            if (model.EmployeeId == Guid.Empty) return Result<ComputeSalaryResponse>.Failure("EmployeeId is required");

            var emp = _employeeContext.GetById(model.EmployeeId);

            if (emp == null)
                return Result<ComputeSalaryResponse>.Failure($"Sorry, employee  not found");

            var sal = new Result<ComputeSalaryResponse>();

            switch (emp.EmployeeType.SalaryType)
            {
                case SalaryType.RegularStaff:
                    sal = _salaryCalculator.ComputeForRegularStaff(emp.EmployeeType, model.DaysAbsent, model.Month);
                    break;

                case SalaryType.ContractualStaff:
                    sal = _salaryCalculator.ComputeForContractualStaff(emp.EmployeeType, model.DaysWorked, model.Month);
                    break;

                default:
                    return Result<ComputeSalaryResponse>.Failure(null, "Salary Type Not Found");
            }

            //save to paymeent
            _ = _paymentManager.Create(new Payment
            {
                Month = sal.Data.Month.ToUpper(),
                Days = sal.Data.Days,
                Employee = emp,
                EmployeeId = emp.Id,
                SalaryPaid = sal.Data.Salary,
                Tax = sal.Data.Tax
            });

            return sal;
        }

        public Result<Guid> Create(EmployeeDTO employee)
        {
            if (string.IsNullOrEmpty(employee.Name)) return Result<Guid>.Failure("Full name is required");
            if (string.IsNullOrEmpty(employee.TIN)) return Result<Guid>.Failure("TIN is required");
            if (employee.EmployeeTypeId == Guid.Empty) return Result<Guid>.Failure("Employee Type is required");
            if (DateTime.Now.Year - employee.BirthDate.Year < 15 ) return Result<Guid>.Failure("Age must be above 15");

            //Validation
            var employees = _employeeContext.GetAll();
            if (employees.FirstOrDefault(x => x.Name.ToLower() == employee.Name.ToLower()) != null)
                return Result<Guid>.Failure($"Sorry, employee with name '{employee.Name}' already exist");

            if (employees.FirstOrDefault(x => x.TIN.ToLower() == employee.TIN.ToLower()) != null)
                return Result<Guid>.Failure($"Sorry, employee with TIN '{employee.TIN}' already exist");

            var empType = _employeeTypeContext.GetById(employee.EmployeeTypeId);

            if (empType == null)
                return Result<Guid>.Failure($"Sorry, employee Type Not Found");

            var save = _employeeContext.Add(new Employee
            {
                Name = employee.Name,
                TIN = employee.TIN,
                EmployeeType = empType,
                BirthDate = employee.BirthDate
            });

            return Result<Guid>.Success(save, "Employee created successfully");
        }

        public Result<bool> Delete(Guid employeeId)
        {
            if (employeeId == Guid.Empty) return Result<bool>.Failure("Employee Id is required");

            var employee = _employeeContext.GetById(employeeId);

            if (employee == null)
                return Result<bool>.Failure($"Sorry, employee  not found");

            employee.IsDeleted = true;

            var save = _employeeContext.Update(employee);

            if (save)
                return Result<bool>.Success(save, "Employee Deleted Successfully");
            else
                return Result<bool>.Failure(save, "Employee Not Deleted");
        }

        public Result<Employee> Get(Guid employeeId)
        {
            if (employeeId == Guid.Empty) return Result<Employee>.Failure("EmployeeId is required");

            var employee = _employeeContext.GetById(employeeId);

            if (employee == null)
                return Result<Employee>.Failure($"Sorry, employee  not found");

            return Result<Employee>.Success(employee, "Employee found");
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employeeContext.GetAll();
        }

        public Result<Employee> Update(EmployeeDTO employee)
        {
            if (string.IsNullOrEmpty(employee.Name)) return Result<Employee>.Failure("Full name is required");
            if (string.IsNullOrEmpty(employee.TIN)) return Result<Employee>.Failure("TIN is required");
            if (employee.EmployeeTypeId == Guid.Empty) return Result<Employee>.Failure("Employee Type is required");
            if (employee.EmployeeId == Guid.Empty) return Result<Employee>.Failure("EmployeeId is required");
            if (DateTime.Now.Year - employee.BirthDate.Year > 15) return Result<Employee>.Failure("Age must be above 15");

            //Validation
            var emp = _employeeContext.GetById(employee.EmployeeId);

            if (emp == null)
                return Result<Employee>.Failure($"Sorry, employee  not found");

            var employees = _employeeContext.GetAll().Where(x => x.Id != employee.EmployeeId);
            if (employees.FirstOrDefault(x => x.Name.ToLower() == employee.Name.ToLower()) != null)
                return Result<Employee>.Failure($"Sorry, employee with name '{employee.Name}' already exist");

            if (employees.FirstOrDefault(x => x.TIN.ToLower() == employee.TIN.ToLower()) != null)
                return Result<Employee>.Failure($"Sorry, employee with TIN '{employee.TIN}' already exist");

            var empType = _employeeTypeContext.GetById(employee.EmployeeTypeId);

            if (empType == null)
                return Result<Employee>.Failure($"Sorry, employee Type Not Found");

            emp.Name = employee.Name;
            emp.TIN = employee.TIN;
            emp.BirthDate = employee.BirthDate;
            emp.EmployeeType = empType;

            var save = _employeeContext.Update(emp);

            return Result<Employee>.Success(emp, "Employee Updated successfully");
        }
    }
}
