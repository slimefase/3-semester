namespace EmployeeApp.Core.Strategies
{
    public class CommissionBankService : IBankService
    {
        public string Name { get; private set; }

        /// <summary>
        /// Процент комиссии для отображения в интерфейсе
        /// </summary>
        public double Commission { get; private set; }

        public CommissionBankService(string name, double commission)
        {
            Name = name;
            Commission = commission;
        }

        /// <summary>
        /// Рассчитывает сумму за вычетом комиссии банка
        /// </summary>
        public double CalculateSalary(double baseSalary)
        {
            return baseSalary - (baseSalary * (Commission / 100.0));
        }
    }
}