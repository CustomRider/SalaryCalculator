namespace SalaryCalculator.Entities
{
    public sealed class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Department Department { get; set; }
    }
}
