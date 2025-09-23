using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Model
{
    /// <summary>
    /// Класс бизнес-логики для управления играми и выполнения CRUD-операций через репозиторий.
    /// </summary>
    public class Logic
    {
        private readonly IRepository<Game> _repository;

        /// <summary>
        /// Инициализирует новый экземпляр класса Logic с указанным репозиторием.
        /// </summary>
        /// <param name="repository">Репозиторий для доступа к данным игр</param>
        public Logic(IRepository<Game> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Создает и добавляет новую игру в базу данных.
        /// </summary>
        public Game CreateGame(string title, string genre, decimal price, decimal discountPercentage = 0)
        {
            if (price < 0)
                throw new ArgumentException("Цена не может быть отрицательной.", nameof(price));
            if (discountPercentage < 0 || discountPercentage > 100)
                throw new ArgumentException("Процент скидки должен быть в диапазоне от 0 до 100.", nameof(discountPercentage));

            var game = new Game
            {
                Title = title,
                Genre = genre,
                Price = price,
                DiscountPercentage = discountPercentage
            };
            return _repository.Add(game);
        }

        /// <summary>
        /// Возвращает игру по ее уникальному идентификатору.
        /// </summary>
        public Game? ReadGame(int id)
        {
            return _repository.ReadById(id);
        }

        /// <summary>
        /// Возвращает список всех игр.
        /// </summary>
        public List<Game> GetAllGames()
        {
            return _repository.ReadAll().ToList();
        }

        /// <summary>
        /// Обновляет данные существующей игры.
        /// </summary>
        public bool UpdateGame(int id, string newTitle, string newGenre, decimal newPrice, decimal newDiscountPercentage)
        {
            if (newPrice < 0)
                throw new ArgumentException("Цена не может быть отрицательной.", nameof(newPrice));
            if (newDiscountPercentage < 0 || newDiscountPercentage > 100)
                throw new ArgumentException("Процент скидки должен быть в диапазоне от 0 до 100.", nameof(newDiscountPercentage));

            var game = _repository.ReadById(id);
            if (game == null)
                return false;

            game.Title = newTitle;
            game.Genre = newGenre;
            game.Price = newPrice;
            game.DiscountPercentage = newDiscountPercentage;
            _repository.Update(game);

            return true;
        }

        /// <summary>
        /// Удаляет игру из базы данных по ее идентификатору.
        /// </summary>
        public bool DeleteGame(int id)
        {
            var game = _repository.ReadById(id);
            if (game == null)
                return false;

            _repository.Delete(id);
            return true;
        }

        /// <summary>
        /// Группирует все игры по жанрам.
        /// </summary>
        public Dictionary<string, List<Game>> GroupGamesByGenre()
        {
            return _repository.ReadAll()
                .GroupBy(g => g.Genre)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        /// <summary>
        /// Возвращает список всех игр, на которые установлена скидка.
        /// </summary>
        public List<Game> GetGamesWithDiscount()
        {
            return _repository.ReadAll()
                .Where(g => g.DiscountPercentage > 0)
                .ToList();
        }
    }
}
