using GameStore.Entity;
using GameStore.DataAccessLayer;
using GameStore.BusinessLogic.Interfaces;
using System.Collections.Generic;

namespace GameStore.BusinessLogic
{
    public class Logic
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
        public void Add(Game game) => _repository.Add(game);
        public void Delete(Game game) => _repository.Delete(game);
        public List<Game> ReadAll() => _repository.ReadAll();
        public Game ReadById(int id) => _repository.ReadById(id);
        public void Update(Game game) => _repository.Update(game);
        public List<Game> GetDiscountedGames() => _discountService.GetDiscountedGames();
        public Dictionary<string, List<Game>> GroupByGenre() => _groupingService.GroupByGenre();
    }
}
