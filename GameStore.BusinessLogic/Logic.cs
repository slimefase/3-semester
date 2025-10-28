using GameStore.Entity;
using GameStore.DataAccessLayer;

namespace GameStore.BusinessLogic
{
    public class Logic
    {
        private readonly IGameRepository _repository;

        public Logic(IGameRepository repo)
        {
            _repository = repo;
        }

        public void Add(Game game) => _repository.Add(game);
        public void Delete(Game game) => _repository.Delete(game);
        public List<Game> ReadAll() => _repository.ReadAll();
        public Game ReadById(int id) => _repository.ReadById(id);
        public void Update(Game game) => _repository.Update(game);
        
        /// <summary>
        /// Возвращает список игр, у которых есть скидка.
        /// </summary>
        public List<Game> GetDiscountedGames()
        {
            return _repository.ReadAll()
                              .Where(g => g.DiscountPercentage > 0)
                              .ToList();
        }

        /// <summary>
        /// Группирует игры по жанру.
        /// </summary>
        public Dictionary<string, List<Game>> GroupByGenre()
        {
            return _repository.ReadAll()
                              .GroupBy(g => g.Genre)
                              .ToDictionary(g => g.Key, g => g.ToList());
        }
    }
}
