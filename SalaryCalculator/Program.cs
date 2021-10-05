using SalaryCalculator.Entities;
using SalaryCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalaryCalculator
{
    class Program
    {
        private static IEnumerable<Department> _departments;
        private static IEnumerable<Employee> _employees;
        private static IEnumerable<AccountingReport> _accountingReports;

        private static readonly int MONTH_FOR_CALCULATION = 3;
        private static readonly int MIN_GROUPING_SALARY = 50000;

        static void Main(string[] args)
        {
            SeedInitialData();
            PrintInitialData();

            var departmentsRating = CalculateDepartmentRating(_accountingReports);
            PrintDepartmentRating(departmentsRating);
        }

        private static IEnumerable<DepartmentRating> CalculateDepartmentRating(IEnumerable<AccountingReport> accountingReports)
        {
           return accountingReports
                               .GroupBy(x => x.Employee)
                               .Where(x => x.All(x => x.Salary > MIN_GROUPING_SALARY))
                               .Select(x =>
                                   new EmployeeSalary
                                   {
                                       Employee = x.Key,
                                       AvgSalary = x.OrderByDescending(x => x.Date)
                                                 .Take(MONTH_FOR_CALCULATION)
                                                 .Average(x => x.Salary)
                                   })
                               .GroupBy(x => x.Employee.Department)
                               .Select(x =>
                                   new DepartmentRating
                                   {
                                       Department = x.Key,
                                       EmployeeSalaries = x.OrderByDescending(x => x.AvgSalary).Take(3).ToList()
                                   });
        }

        private static void PrintDepartmentRating(IEnumerable<DepartmentRating> departmentRatings)
        {
            foreach (var departmentRating in departmentRatings)
            {
                Console.WriteLine($"Depatment: {departmentRating.Department.Name}");
                foreach (var employee in departmentRating.EmployeeSalaries)
                {
                    Console.WriteLine($"   - {employee.Employee.Name} with average salary {employee.AvgSalary:0.00}");
                }
            }
        }

        private static void SeedInitialData()
        {
            _departments = SeedData.Departments(2);
            _employees = SeedData.Employees(10, _departments);
            _accountingReports = SeedData.AccountingReports(_employees, 5);
        }

        private static void PrintInitialData()
        {
            PrintHelper.PrintTitle("Departments:");
            PrintHelper.PrintTable(
                _departments,
                new string[] { nameof(Department.Id), nameof(Department.Name) },
                x => x.Id,
                x => x.Name);

            PrintHelper.PrintTitle("Employees:");
            PrintHelper.PrintTable(
                 _employees,
                 new string[] { nameof(Employee.Id), nameof(Employee.Name), nameof(Employee.Department) },
                 x => x.Id,
                 x => x.Name,
                 x => $"{x.Department.Name} ({x.Department.Id})");

            PrintHelper.PrintTitle("Account reports:");
            PrintHelper.PrintTable(
                 _accountingReports,
                 new string[] { nameof(AccountingReport.Id), nameof(AccountingReport.Date), nameof(AccountingReport.Employee), nameof(AccountingReport.Salary) },
                 x => x.Id,
                 x => x.Date.ToShortDateString(),
                 x => $"{x.Employee.Name} ({x.Employee.Id})",
                 x => x.Salary);
        }
    }
}
