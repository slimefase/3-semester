using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Model
{
    public class Logic
    {
        private readonly List<Game> _games = [];
        private int _nextId = 1;

        /// <summary>
        /// Создает и добавляет новую игру в коллекцию. Реализует требование "Создание сущности"
        /// </summary>
        public Game CreateGame(string title, string genre, decimal price)
        {
            var game = new Game
            {
                Id = _nextId++,
                Title = title,
                Genre = genre,
                Price = price
            };
            _games.Add(game);
            return game;
        }

        /// <summary>
        /// Возвращает игру по ее уникальному идентификатору. Реализует требование "Чтение сущности"
        /// </summary>
        public Game? ReadGame(int id)
        {
            return _games.FirstOrDefault(g => g.Id == id);
        }

        /// <summary>
        /// Возвращает список всех игр. Также относится к "Чтению сущности"
        /// </summary>
        public List<Game> GetAllGames()
        {
            return new List<Game>(_games);
        }

        /// <summary>
        /// Обновляет данные существующей игры. Реализует требование "Изменение сущности"
        /// </summary>
        public bool UpdateGame(int id, string newTitle, string newGenre, decimal newPrice)
        {
            var game = ReadGame(id);
            if (game != null)
            {
                game.Title = newTitle;
                game.Genre = newGenre;
                game.Price = newPrice;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Удаляет игру из коллекции по ее идентификатору. Реализует требование "Удаление сущности"
        /// </summary>
        public bool DeleteGame(int id)
        {
            var game = ReadGame(id);
            if (game != null)
            {
                _games.Remove(game);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Бизнес-функция 1: Группирует все игры по жанрам
        /// </summary>
        public Dictionary<string, List<Game>> GroupGamesByGenre()
        {
            return _games.GroupBy(g => g.Genre)
                         .ToDictionary(g => g.Key, g => g.ToList());
        }

        /// <summary>
        /// Бизнес-функция 2: Возвращает список игр, цена которых ниже указанного значения
        /// </summary>
        public List<Game> GetGamesCheaperThan(decimal maxPrice)
        {
            return _games.Where(g => g.Price < maxPrice).ToList();
        }
    }
}
