using EmployeeApp.Core.Strategies;

namespace EmployeeApp.Core.Entities
{
    public class Employee
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public double BaseSalary { get; set; }
        public IBankService BankService { get; set; }

        public Employee(string name, string position, double baseSalary, IBankService bankService)
        {
            Name = name;
            Position = position;
            BaseSalary = baseSalary;
            BankService = bankService;
        }

        /// <summary>
        /// Возвращает базовую информацию о сотруднике
        /// </summary>
        public virtual string GetInfo()
        {
            return $"{Position}: {Name}";
        }

        /// <summary>
        /// Вычисляет зарплату через стратегию банка
        /// </summary>
        public virtual double CalculateSalary()
        {
            return BankService?.CalculateSalary(BaseSalary) ?? BaseSalary;
        }
    }
}