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
    }
}
