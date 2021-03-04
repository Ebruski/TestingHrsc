using HRSC.Core.Adapters;
using HRSC.Core.Extensions;
using HRSC.Core.Models;
using Xunit;

namespace HRSC.Test
{
    public class SalaryCalculatorTests
    {
        [Theory]
        [InlineData(0.5, 17145.45)]
        [InlineData(1, 16690.91)]
        [InlineData(1.5, 16236.36)]
        [InlineData(2, 15781.82)]
        [InlineData(3, 14872.73)]
        public void ComputeForRegularStaff_ShouldCalculate(double daysAbsent, decimal expected)
        {
            // Arrange
            var employeeType = new EmployeeType
            {
                Amount = 20000.00m,
                HasTax = true
            };

            var appSetting = new AppSettings
            {
                TaxPercentage = 12
            };

            var sc = new SalaryCalculator(appSetting);

            // Act
            decimal salary = sc.ComputeForRegularStaff(employeeType, daysAbsent, "May").Data.Salary;



            // Assert
            Assert.Equal(expected, salary);

        }


        [Theory]
        [InlineData(0.5, 250.00)]
        [InlineData(1, 500.00)]
        [InlineData(15.5, 7750.00)]
        [InlineData(22, 11000.00)]
        public void ComputeForContractualStaff_ShouldCalculate(double daysWorked, decimal expected)
        {
            // Arrange
            var employeeType = new EmployeeType
            {
                Amount = 500.00m,
                HasTax = false
            };

            var sc = new SalaryCalculator(null);

            // Act
            decimal salary = sc.ComputeForContractualStaff(employeeType, daysWorked, "May").Data.Salary;



            // Assert
            Assert.Equal(expected, salary);
        }
    }
}
