using EmployeeApp.Core.Entities;

namespace EmployeeApp.Core.Decorators
{
    public abstract class EmployeeDecorator : Employee
    {
        protected Employee _employee;

        protected EmployeeDecorator(Employee employee)
            : base(employee.Name, employee.Position, employee.BaseSalary, employee.BankService)
        {
            _employee = employee;
        }

        /// <summary>
        /// Делегирует расчет зарплаты обернутому объекту
        /// </summary>
        public override double CalculateSalary()
        {
            return _employee.CalculateSalary();
        }
    }

    public class EnglishCertificateDecorator : EmployeeDecorator
    {
        public string ExaminationTitle { get; private set; }
        public int YearOfCertificate { get; private set; }

        public EnglishCertificateDecorator(Employee employee, string title, int year)
            : base(employee)
        {
            ExaminationTitle = title;
            YearOfCertificate = year;
        }

        /// <summary>
        /// Добавляет данные о сертификате к строке информации
        /// </summary>
        public override string GetInfo()
        {
            return $"{_employee.GetInfo()} [English: {ExaminationTitle}, {YearOfCertificate}]";
        }
    }

    public class AcademicDegreeDecorator : EmployeeDecorator
    {
        public string DissertationTitle { get; private set; }
        public int Year { get; private set; }
        public string ScienceArea { get; private set; }

        public AcademicDegreeDecorator(Employee employee, string title, int year, string area)
            : base(employee)
        {
            DissertationTitle = title;
            Year = year;
            ScienceArea = area;
        }

        /// <summary>
        /// Добавляет данные об ученой степени к строке информации
        /// </summary>
        public override string GetInfo()
        {
            return $"{_employee.GetInfo()} [PhD: {ScienceArea}, '{DissertationTitle}' ({Year})]";
        }
    }
}