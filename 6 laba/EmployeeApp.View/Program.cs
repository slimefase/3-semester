using System;
using EmployeeApp.Core.Entities;
using EmployeeApp.Core.Decorators;
using EmployeeApp.Core.Strategies;
using EmployeeApp.Data;

namespace EmployeeApp.View
{
    class Program
    {
        private static EmployeeRepository _repository = new EmployeeRepository();

        static void Main(string[] args)
        {
            Console.Title = "Система учета сотрудников v1.0";

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==============================================");
                Console.WriteLine("            УПРАВЛЕНИЕ ПЕРСОНАЛОМ ");
                Console.WriteLine("==============================================");
                Console.WriteLine();
                Console.WriteLine(" 1. Добавить нового сотрудника");
                Console.WriteLine(" 2. Список сотрудников и расчет зарплаты");
                Console.WriteLine(" 3. Присвоить регалии (English/PhD)");
                Console.WriteLine(" 4. Изменить обслуживающий банк");
                Console.WriteLine(" 0. Завершить работу");
                Console.WriteLine();
                Console.WriteLine("==============================================");
                Console.WriteLine();
                Console.Write(" >> Выберите пункт меню: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddEmployee(); break;
                    case "2": ShowEmployees(); break;
                    case "3": DecorateEmployee(); break;
                    case "4": ChangeBankStrategy(); break;
                    case "0": Environment.Exit(0); break;
                    default:
                        ShowError("Неверный пункт меню. Нажмите любую клавишу...");
                        break;
                }
            }
        }

        /// <summary>
        /// Проверка на число
        /// </summary>
        private static double ReadDouble(string message)
        {
            while (true)
            {
                Console.Write(message);
                if (double.TryParse(Console.ReadLine(), out double result) && result >= 0)
                    return result;
                ShowError("Ошибка! Введите положительное число.");
            }
        }

        /// <summary>
        /// Проверка на целое число
        /// </summary>
        private static int ReadInt(string message, int min, int max)
        {
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out int result) && result >= min && result <= max)
                    return result;
                ShowError($"Ошибка! Введите число от {min} до {max}.");
            }
        }

        /// <summary>
        /// Ошибка
        /// </summary>
        private static void ShowError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }

        /// <summary>
        /// Создает сотрудника как экземпляр универсального класса с указанием должности
        /// </summary>
        static void AddEmployee()
        {
            Console.Clear();
            Console.WriteLine("--- РЕГИСТРАЦИЯ СОТРУДНИКА ---");

            Console.Write("Имя: ");
            string name = Console.ReadLine();

            double salary = ReadDouble("Ставка: ");

            Console.WriteLine("Выберите должность: 1-Инженер, 2-Менеджер, 3-Ученый");
            int choice = ReadInt(" >> ", 1, 3);

            string position = choice switch
            {
                1 => "Инженер",
                2 => "Менеджер",
                3 => "Ученый",
                _ => "Сотрудник"
            };

            Employee emp = new Employee(name, position, salary, BankRegistry.GetDefault());

            _repository.Add(emp);
            Console.WriteLine("\nСотрудник добавлен.");
            Console.WriteLine("\nНажмите любую клавишу для выхода в меню...");
            Console.ReadKey();
        }

        /// <summary>
        /// вывод сотрудников
        /// </summary>
        static void ShowEmployees()
        {
            Console.Clear();
            var list = _repository.GetAll();

            if (list.Count == 0)
            {
                Console.WriteLine("База данных пуста.");
            }
            else
            {
                Console.WriteLine("{0,-5} | {1,-40} | {2,-15}", "ID", "Информация / Регалии", "К выплате");
                Console.WriteLine(new string('-', 70));

                for (int i = 0; i < list.Count; i++)
                {
                    var e = list[i];
                    Console.WriteLine("{0,-5} | {1,-40} | {2,-15:F2}",
                        i + 1, e.GetInfo(), e.CalculateSalary());
                    Console.WriteLine("{0,-5} | Банк: {1}", "", e.BankService.Name);
                    Console.WriteLine(new string('-', 70));
                }
            }
            Console.WriteLine("\nНажмите любую клавишу для выхода в меню...");
            Console.ReadKey();
        }

        /// <summary>
        /// Применение декораторов с проверкой существования данных
        /// </summary>
        static void DecorateEmployee()
        {
            Console.Clear();
            var list = _repository.GetAll();
            if (list.Count == 0) { ShowError("Нет сотрудников для редактирования."); Console.ReadKey(); return; }

            for (int i = 0; i < list.Count; i++) Console.WriteLine($"{i + 1}. {list[i].Name}");
            int idx = ReadInt("Выберите номер сотрудника: ", 1, list.Count);

            var selectedEmp = list[idx - 1];

            Console.WriteLine("\nКакую квалификацию добавить?");
            Console.WriteLine("1. Сертификат английского");
            Console.WriteLine("2. Ученая степень");
            Console.WriteLine("0. Отмена");

            int type = ReadInt(" >> ", 0, 2);
            if (type == 0) return;

            Employee newVersion = selectedEmp;

            if (type == 1)
            {
                Console.Write("Экзамен (напр. IELTS): ");
                string title = Console.ReadLine();
                int year = ReadInt("Год получения: ", 1990, DateTime.Now.Year);
                newVersion = new EnglishCertificateDecorator(selectedEmp, title, year);
            }
            else
            {
                Console.Write("Область наук: ");
                string area = Console.ReadLine();
                Console.Write("Тема диссертации: ");
                string theme = Console.ReadLine();
                int year = ReadInt("Год защиты: ", 1950, DateTime.Now.Year);
                newVersion = new AcademicDegreeDecorator(selectedEmp, theme, year, area);
            }

            _repository.Update(idx - 1, newVersion);
            Console.WriteLine("Данные обновлены.");
            Console.ReadKey();
        }

        /// <summary>
        /// Меняет стратегию банка с отображением комиссии в скобках
        /// </summary>
        static void ChangeBankStrategy()
        {
            Console.Clear();
            var list = _repository.GetAll();
            if (list.Count == 0) { ShowError("Список сотрудников пуст."); Console.ReadKey(); return; }

            for (int i = 0; i < list.Count; i++)
                Console.WriteLine($"{i + 1}. {list[i].Name} (Текущий: {list[i].BankService.Name})");

            int empIdx = ReadInt("Выберите сотрудника: ", 1, list.Count);
            var selectedEmp = list[empIdx - 1];

            var banks = BankRegistry.Banks;
            Console.WriteLine("\nДоступные банки:");

            for (int i = 0; i < banks.Count; i++)
            {
                string commissionInfo = "";

                if (banks[i] is CommissionBankService cbs)
                {
                    commissionInfo = $" ({cbs.Commission}%)";
                }

                Console.WriteLine($"{i + 1}. {banks[i].Name}{commissionInfo}");
            }

            int bankIdx = ReadInt("Выберите новый банк: ", 1, banks.Count);

            selectedEmp.BankService = banks[bankIdx - 1];

            Console.WriteLine($"\nУспешно! Теперь зарплата для {selectedEmp.Name} считается через {selectedEmp.BankService.Name}.");
            Console.WriteLine("Нажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }
    }
}