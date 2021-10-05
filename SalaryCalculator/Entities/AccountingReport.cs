using System;

namespace SalaryCalculator.Entities
{
    public sealed class AccountingReport
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public DateTime Date { get; set; }
        public decimal Salary { get; set; }
    }
}
