using HRSC.Core.Adapters.Interfaces;
using HRSC.Core.DTO;
using HRSC.Core.Extensions;
using HRSC.Core.Models;
using System;

namespace HRSC.Core.Adapters
{
    public class SalaryCalculator : ISalaryCalculator
    {
        private AppSettings _appSettings;

        public SalaryCalculator(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public Result<ComputeSalaryResponse> ComputeForContractualStaff(EmployeeType empType, double daysWorked, string month)
        {
            decimal salary = empType.Amount * Convert.ToDecimal(daysWorked);

            decimal tax = 0.00m;

            if (empType.HasTax)
            {
                tax = salary * (_appSettings.TaxPercentage / 100);

                salary = salary - tax;
            }                

            var rsp = new ComputeSalaryResponse
            {
                Salary = Math.Round(salary, 2),
                Month = month,
                Tax = Math.Round(tax, 2),
                Days = daysWorked
            };

            return Result<ComputeSalaryResponse>.Success(rsp, $"Salary Calculated for the Month of {month.ToUpper()}");


        }

        public Result<ComputeSalaryResponse> ComputeForRegularStaff(EmployeeType empType, double daysAbsent, string month)
        {
            decimal salary = empType.Amount;

            decimal tax = 0.00m;

            if (daysAbsent > 0)
                salary = salary - ((empType.Amount / 22) * Convert.ToDecimal(daysAbsent));

            if (empType.HasTax)
            {
                tax = empType.Amount * (_appSettings.TaxPercentage / 100);

                salary = salary - tax;
            }

            var rsp = new ComputeSalaryResponse
            {
                Salary = Math.Round(salary, 2),
                Month = month,
                Tax = tax,
                Days = daysAbsent
            };

            return Result<ComputeSalaryResponse>.Success(rsp, $"Salary Calculated for the Month of {month.ToUpper()}");

        }
    }
}