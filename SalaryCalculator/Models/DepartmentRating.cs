using SalaryCalculator.Entities;
using System.Collections.Generic;

namespace SalaryCalculator.Models
{
    public class DepartmentRating
    {
        public Department Department { get; set; }
        public List<EmployeeSalary> EmployeeSalaries { get; set; }
    }
}
