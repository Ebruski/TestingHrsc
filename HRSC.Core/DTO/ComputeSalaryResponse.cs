namespace HRSC.Core.DTO
{
    public class ComputeSalaryResponse
    {
        public decimal Salary { get; set; }
        public decimal Tax { get; set; }
        public double Days { get; set; }
        public string Month { get; set; }
    }
}
