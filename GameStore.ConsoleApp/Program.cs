using GameStore.Model;
using System;
using System.Linq;

namespace GameStore.ConsoleApp
{
    internal class Program
    {
        // Создаем единственный экземпляр логики для всего приложения
        private static readonly Logic gameLogic = new Logic();

        static void Main(string[] args)
        {
            // Загружаем начальные данные для демонстрации
            LoadInitialData();

            while (true)
            {
                Console.WriteLine("\n------ Магазин игр Steam ------");
                Console.WriteLine("1. Показать все игры");
                Console.WriteLine("2. Добавить новую игру");
                Console.WriteLine("3. Удалить игру");
                Console.WriteLine("4. Сгруппировать игры по жанру");
                Console.WriteLine("5. Показать игры дешевле 500 руб.");
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
                        DeleteGame();
                        break;
                    case "4":
                        ShowGamesGroupedByGenre();
                        break;
                    case "5":
                        ShowGamesCheaperThan500();
                        break;
                    case "0":
                        return; // Выход из приложения
                    default:
                        Console.WriteLine("Неверный ввод. Пожалуйста, попробуйте снова.");
                        break;
                }
            }
        }

        /// <summary>
        /// Загружает начальный набор данных.
        /// </summary>
        private static void LoadInitialData()
        {
            gameLogic.CreateGame("Stardew Valley", "Simulator", 299m);
            gameLogic.CreateGame("Hades", "Roguelike", 899m);
            gameLogic.CreateGame("Factorio", "Simulator", 520m);
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
                Console.WriteLine($"ID: {game.Id}, Название: {game.Title}, Жанр: {game.Genre}, Цена: {game.Price:F2} руб.");
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
                Console.Write("Введите жанр: ");
                string genre = Console.ReadLine();
                Console.Write("Введите цену: ");
                decimal price = decimal.Parse(Console.ReadLine());

                var newGame = gameLogic.CreateGame(title, genre, price);
                Console.WriteLine($"Игра '{newGame.Title}' успешно добавлена!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: неверный формат цены.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
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

                if (gameLogic.DeleteGame(id))
                {
                    Console.WriteLine($"Игра с ID {id} успешно удалена.");
                }
                else
                {
                    Console.WriteLine($"Игра с ID {id} не найдена.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: ID должен быть числом.");
            }
        }

        /// <summary>
        /// Выводит в консоль игры, сгруппированные по жанру (Бизнес-функция 1).
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
        /// Выводит в консоль игры дешевле 500 рублей (Бизнес-функция 2).
        /// </summary>
        private static void ShowGamesCheaperThan500()
        {
            decimal priceLimit = 500m;
            var cheapGames = gameLogic.GetGamesCheaperThan(priceLimit);
            Console.WriteLine($"\n--- Игры дешевле {priceLimit} руб. ---");
            if (!cheapGames.Any())
            {
                Console.WriteLine("Таких игр нет.");
                return;
            }
            foreach (var game in cheapGames)
            {
                Console.WriteLine($"- {game.Title} ({game.Price:F2} руб.)");
            }
        }
    }
}