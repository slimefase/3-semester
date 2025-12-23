namespace EmployeeApp.Core.Strategies
{
    public interface IBankService
    {
        /// <summary>
        /// Название банка
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Вычисляет итоговую сумму перевода с учетом комиссии
        /// </summary>
        double CalculateSalary(double baseSalary);
    }
}