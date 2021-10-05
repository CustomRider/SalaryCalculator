using SalaryCalculator.Entities;

namespace SalaryCalculator.Models
{
    public class EmployeeSalary
    {
        public Employee Employee { get; set; }
        public decimal AvgSalary { get; set; }
    }
}
