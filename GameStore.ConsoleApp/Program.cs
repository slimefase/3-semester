using GameStore.BusinessLogic;
using GameStore.Entity;
using Ninject;

namespace GameStore.ConsoleApp
{
    internal class Program
    {
        private static Logic logic;

        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new SimpleConfigModule());
            logic = kernel.Get<Logic>();

            RunMenu();
        }

        /// <summary>
        /// Основное меню консольного приложения.
        /// </summary>
        static void RunMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("------ Магазин игр ------");
                Console.WriteLine("1. Показать все игры");
                Console.WriteLine("2. Добавить игру");
                Console.WriteLine("3. Удалить игру");
                Console.WriteLine("4. Изменить игру");
                Console.WriteLine("5. Игры со скидкой");
                Console.WriteLine("6. Группировка по жанрам");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                Console.Clear();

                switch (choice)
                {
                    case "1": ShowAllGames(); break;
                    case "2": AddGame(); break;
                    case "3": DeleteGame(); break;
                    case "4": UpdateGame(); break;
                    case "5": ShowDiscountedGames(); break;
                    case "6": GroupGamesByGenre(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Показать все игры.
        /// </summary>
        static void ShowAllGames()
        {
            var games = logic.ReadAll();
            if (games.Count == 0)
            {
                Console.WriteLine("База данных пуста.");
                return;
            }

            foreach (var g in games)
                Console.WriteLine($"{g.Id}: {g.Title} | {g.Genre} | {g.Price} руб. | Скидка {g.DiscountPercentage}%");
        }

        /// <summary>
        /// Добавить игру.
        /// </summary>
        static void AddGame()
        {
            Console.Write("Название: ");
            var title = Console.ReadLine();
            Console.Write("Жанр: ");
            var genre = Console.ReadLine();
            Console.Write("Цена: ");
            var price = decimal.Parse(Console.ReadLine());
            Console.Write("Скидка (%): ");
            var discount = decimal.Parse(Console.ReadLine());

            logic.Add(new Game { Title = title, Genre = genre, Price = price, DiscountPercentage = discount });
            Console.WriteLine("Игра добавлена.");
        }

        /// <summary>
        /// Удалить игру.
        /// </summary>
        static void DeleteGame()
        {
            Console.Write("Id для удаления: ");
            var id = int.Parse(Console.ReadLine());
            var game = logic.ReadById(id);
            if (game == null)
            {
                Console.WriteLine("Игра не найдена.");
                return;
            }

            logic.Delete(game);
            Console.WriteLine("Игра удалена.");
        }

        /// <summary>
        /// Обновить игру.
        /// </summary>
        static void UpdateGame()
        {
            Console.Write("Id для редактирования: ");
            var id = int.Parse(Console.ReadLine());
            var game = logic.ReadById(id);
            if (game == null)
            {
                Console.WriteLine("Игра не найдена.");
                return;
            }

            Console.Write("Новое название: ");
            game.Title = Console.ReadLine();
            Console.Write("Новый жанр: ");
            game.Genre = Console.ReadLine();
            Console.Write("Новая цена: ");
            game.Price = decimal.Parse(Console.ReadLine());
            Console.Write("Новая скидка (%): ");
            game.DiscountPercentage = decimal.Parse(Console.ReadLine());

            logic.Update(game);
            Console.WriteLine("Игра обновлена.");
        }

        /// <summary>
        /// Показать все игры со скидкой.
        /// </summary>
        static void ShowDiscountedGames()
        {
            var discounted = logic.GetDiscountedGames();
            if (discounted.Count == 0)
            {
                Console.WriteLine("Нет игр со скидкой.");
                return;
            }

            foreach (var g in discounted)
            {
                Console.WriteLine($"{g.Title} — {g.DiscountPercentage}% (цена со скидкой: {g.DiscountedPrice} руб.)");
            }
        }

        /// <summary>
        /// Сгруппировать игры по жанрам.
        /// </summary>
        static void GroupGamesByGenre()
        {
            var grouped = logic.GroupByGenre();
            if (grouped.Count == 0)
            {
                Console.WriteLine("Нет данных для группировки.");
                return;
            }

            foreach (var genre in grouped)
            {
                Console.WriteLine($"\n{genre.Key}:");
                foreach (var g in genre.Value)
                {
                    Console.WriteLine($"  - {g.Title}");
                }
            }
        }
    }
}
