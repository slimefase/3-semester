using GameStore.DataAccessLayer;
using GameStore.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;

namespace GameStore.ConsoleApp
{
    internal class Program
    {
        private static Logic gameLogic;

        static void Main(string[] args)
        {
            string dbPath = @"C:\Учёба\Архитектура информационных систем\3-semester\GameStoreDatabase.mdf";

            var connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};Initial Catalog=MySharedGameStoreDB;Integrated Security=True;Connect Timeout=30";

            InitializeDatabase(connectionString);
            LoadInitialData();

            while (true)
            {
                Console.WriteLine("\n------ Магазин игр Steam ------");
                Console.WriteLine("1. Показать все игры");
                Console.WriteLine("2. Добавить новую игру");
                Console.WriteLine("3. Изменить игру");
                Console.WriteLine("4. Удалить игру");
                Console.WriteLine("5. Сгруппировать игры по жанру");
                Console.WriteLine("6. Показать все игры со скидкой");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllGames();
                        break;
                    case "2":
                        AddNewGame();
                        break;
                    case "3":
                        UpdateGameConsole();
                        break;
                    case "4":
                        DeleteGame();
                        break;
                    case "5":
                        ShowGamesGroupedByGenre();
                        break;
                    case "6":
                        ShowDiscountedGames();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный ввод. Пожалуйста, попробуйте снова.");
                        break;
                }
            }
        }

        /// <summary>
        /// Инициализирует контекст базы данных и репозиторий.
        /// </summary>
        /// <param name="connectionString">Строка подключения к базе данных.</param>
        private static void InitializeDatabase(string connectionString)
        {
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(connectionString)
                .Options;

            using (var context = new DBContext(options))
            {
                try
                {
                    context.Database.EnsureCreated();
                }
                catch (SqlException ex)
                {
                    // Игнорируем ошибку, если база данных уже существует и прикреплена
                    if (!ex.Message.Contains("уже существует") && !ex.Message.Contains("already exists"))
                    {
                        throw; // Если ошибка другая, ее нужно показать
                    }
                }
            }

            IRepository<Game> repository = new EntityRepository<Game>(new DBContext(options));
            gameLogic = new Logic(repository);
        }

        /// <summary>
        /// Загружает начальный набор данных, если база пуста.
        /// </summary>
        private static void LoadInitialData()
        {
            var existingGames = gameLogic.GetAllGames();
            if (!existingGames.Any())
            {
                gameLogic.CreateGame("Stardew Valley", "Simulator", 299m, 15);
                gameLogic.CreateGame("Hades", "Roguelike", 899m);
                gameLogic.CreateGame("Factorio", "Simulator", 520m, 20);
            }
        }

        /// <summary>
        /// Выводит список всех игр в консоль.
        /// </summary>
        private static void ShowAllGames()
        {
            var games = gameLogic.GetAllGames();
            Console.WriteLine("\n--- Список всех игр ---");
            if (!games.Any())
            {
                Console.WriteLine("В магазине пока нет игр.");
                return;
            }
            foreach (var game in games)
            {
                string discountInfo = game.DiscountPercentage > 0 ? $" (Скидка {game.DiscountPercentage}%, Новая цена: {game.DiscountedPrice:F2} руб.)" : "";
                Console.WriteLine($"ID: {game.Id}, {game.Title} ({game.Genre}) - {game.Price:F2} руб.{discountInfo}");
            }
        }

        /// <summary>
        /// Запрашивает у пользователя данные для создания новой игры.
        /// </summary>
        private static void AddNewGame()
        {
            try
            {
                Console.WriteLine("\n--- Добавление новой игры ---");

                Console.Write("Введите название: ");
                string title = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(title)) { Console.WriteLine("Ошибка: Название не может быть пустым."); return; }

                Console.Write("Введите жанр: ");
                string genre = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(genre)) { Console.WriteLine("Ошибка: Жанр не может быть пустым."); return; }

                Console.Write("Введите цену: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price)) { Console.WriteLine("Ошибка: неверный формат цены."); return; }

                Console.Write("Введите скидку в % (или 0, если нет): ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal discount)) { Console.WriteLine("Ошибка: неверный формат скидки."); return; }

                var newGame = gameLogic.CreateGame(title, genre, price, discount);
                Console.WriteLine($"Игра '{newGame.Title}' успешно добавлена!");
            }
            catch (ArgumentException ex) { Console.WriteLine($"Ошибка валидации: {ex.Message}"); }
            catch (Exception ex) { Console.WriteLine($"Произошла непредвиденная ошибка: {ex.Message}"); }
        }

        /// <summary>
        /// Запрашивает у пользователя данные для изменения существующей игры.
        /// </summary>
        private static void UpdateGameConsole()
        {
            try
            {
                Console.Write("\nВведите ID игры для изменения: ");
                if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Ошибка: ID должен быть числом."); return; }

                var game = gameLogic.ReadGame(id);
                if (game == null) { Console.WriteLine($"Игра с ID {id} не найдена."); return; }

                Console.WriteLine($"--- Изменение игры: {game.Title} ---");

                Console.Write($"Новое название (Enter, чтобы оставить '{game.Title}'): ");
                string newTitle = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newTitle)) newTitle = game.Title;

                Console.Write($"Новый жанр (Enter, чтобы оставить '{game.Genre}'): ");
                string newGenre = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newGenre)) newGenre = game.Genre;

                Console.Write($"Новая цена (Enter, чтобы оставить '{game.Price:F2}'): ");
                string priceStr = Console.ReadLine();
                if (!decimal.TryParse(priceStr, out decimal newPrice)) newPrice = game.Price;

                Console.Write($"Новая скидка в % (Enter, чтобы оставить '{game.DiscountPercentage}'): ");
                string discountStr = Console.ReadLine();
                if (!decimal.TryParse(discountStr, out decimal newDiscount)) newDiscount = game.DiscountPercentage;

                if (gameLogic.UpdateGame(id, newTitle, newGenre, newPrice, newDiscount))
                {
                    Console.WriteLine("Игра успешно обновлена!");
                }
            }
            catch (ArgumentException ex) { Console.WriteLine($"Ошибка валидации: {ex.Message}"); }
            catch (Exception ex) { Console.WriteLine($"Произошла ошибка: {ex.Message}"); }
        }

        /// <summary>
        /// Запрашивает у пользователя ID для удаления игры.
        /// </summary>
        private static void DeleteGame()
        {
            try
            {
                Console.WriteLine("\n--- Удаление игры ---");
                Console.Write("Введите ID игры для удаления: ");
                int id = int.Parse(Console.ReadLine());

                if (gameLogic.DeleteGame(id)) { Console.WriteLine($"Игра с ID {id} успешно удалена."); }
                else { Console.WriteLine($"Игра с ID {id} не найдена."); }
            }
            catch (FormatException) { Console.WriteLine("Ошибка: ID должен быть числом."); }
        }

        /// <summary>
        /// Выводит в консоль игры, сгруппированные по жанру.
        /// </summary>
        private static void ShowGamesGroupedByGenre()
        {
            var groupedGames = gameLogic.GroupGamesByGenre();
            Console.WriteLine("\n--- Группировка по жанрам ---");
            foreach (var group in groupedGames)
            {
                Console.WriteLine($"\nЖанр: {group.Key}");
                foreach (var game in group.Value)
                {
                    Console.WriteLine($"\t- {game.Title}");
                }
            }
        }

        /// <summary>
        /// Выводит в консоль список игр, на которые действует скидка.
        /// </summary>
        private static void ShowDiscountedGames()
        {
            var discountedGames = gameLogic.GetGamesWithDiscount();
            Console.WriteLine("\n--- Игры со скидкой ---");
            if (!discountedGames.Any())
            {
                Console.WriteLine("На данный момент игр со скидкой нет.");
                return;
            }

            foreach (var game in discountedGames)
            {
                Console.WriteLine($"- {game.Title}, скидка {game.DiscountPercentage}%. Старая цена: {game.Price:F2} руб. -> Новая цена: {game.DiscountedPrice:F2} руб.");
            }
        }
    }
}
