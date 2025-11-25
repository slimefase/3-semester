using GameStore.Entity;
using GameStore.DataAccessLayer;
using GameStore.BusinessLogic.Interfaces;
using System.Collections.Generic;
using System.Linq;
using GameStore.Shared;

namespace GameStore.BusinessLogic
{
    public class Logic : IModel
    {
        private readonly IGameRepository _repository;
        private readonly IDiscountService _discountService;
        private readonly IGroupingService _groupingService;

        public Logic(IGameRepository repo, IDiscountService discountService, IGroupingService groupingService)
        {
            _repository = repo;
            _discountService = discountService;
            _groupingService = groupingService;
        }

        /// <summary>
        /// Добавляет игру
        /// </summary>
        public void Add(Game game) => _repository.Add(game);

        /// <summary>
        /// Удаляет игру
        /// </summary>
        public void Delete(Game game) => _repository.Delete(game);

        /// <summary>
        /// Возвращает все игры
        /// </summary>
        public List<Game> ReadAll() => _repository.ReadAll();

        /// <summary>
        /// Возвращает игру по идентификатору
        /// </summary>
        public Game ReadById(int id) => _repository.ReadById(id);

        /// <summary>
        /// Обновляет игру
        /// </summary>
        public void Update(Game game) => _repository.Update(game);

        /// <summary>
        /// Возвращает игры со скидкой
        /// </summary>
        public List<Game> GetDiscountedGames() => _discountService.GetDiscountedGames();

        /// <summary>
        /// Группирует игры по жанру
        /// </summary>
        public Dictionary<string, List<Game>> GroupByGenre() => _groupingService.GroupByGenre();
    }
}
