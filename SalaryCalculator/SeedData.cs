using Bogus;
using Bogus.DataSets;
using SalaryCalculator.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalaryCalculator
{
    public static class SeedData
    {
        public static IEnumerable<Department> Departments(int count)
        {
            var departmentsIds = 0;
            var fakeDepartments = new Faker<Department>()
                .RuleFor(x => x.Id, x => departmentsIds++)
                .RuleFor(x => x.Name, x => x.Name.JobArea());
            return fakeDepartments.Generate(count);
        }

        public static IEnumerable<Employee> Employees(int count, IEnumerable<Department> departments)
        {
            var employeeIds = 0;
            var fakeEmployee = new Faker<Employee>()
                .RuleFor(x => x.Id, x => employeeIds++)
                .RuleFor(x => x.Name, (f, u) => $"{f.Name.FirstName(Name.Gender.Male)} {f.Name.LastName(Name.Gender.Male)}")
                .RuleFor(x => x.Department, x => departments.ElementAt(x.Random.Number(0, departments.Count() - 1)));
            return fakeEmployee.Generate(count);
        }

        public static IEnumerable<AccountingReport> AccountingReports(IEnumerable<Employee> employees, int countMonth)
        {
            var accountingReportIds = 0;
            var rnd = new Random();
            foreach (var employee in employees)
            {
                for (int i = 1; i < countMonth + 1; i++)
                {
                    yield return new AccountingReport
                    {
                        Id = accountingReportIds++,
                        Date = DateTime.Now.AddMonths(-i).Date,
                        Employee = employee,
                        Salary = rnd.Next(45000, 100000)
                    };
                };
            }
        }
    }
}
