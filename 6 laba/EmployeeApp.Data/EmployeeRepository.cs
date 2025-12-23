using System.Collections.Generic;
using System.Linq;
using EmployeeApp.Core.Entities;

namespace EmployeeApp.Data
{
    public class EmployeeRepository
    {
        private readonly List<Employee> _employees;

        public EmployeeRepository()
        {
            _employees = new List<Employee>();
        }

        /// <summary>
        /// Добавляет сотрудника в базу
        /// </summary>
        public void Add(Employee employee)
        {
            _employees.Add(employee);
        }

        /// <summary>
        /// Получает всех сотрудников
        /// </summary>
        public List<Employee> GetAll()
        {
            return _employees;
        }

        /// <summary>
        /// Заменяет сотрудника
        /// </summary>
        public void Update(int index, Employee newEmployeeVersion)
        {
            if (index >= 0 && index < _employees.Count)
            {
                _employees[index] = newEmployeeVersion;
            }
        }
    }
}