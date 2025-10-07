namespace GameStore.Model
{
    public class Logic
    {
        private readonly IRepository _repository;

        /// <summary>
        /// Инициализация с репозиторием (передается снаружи).
        /// </summary>
        public Logic(IRepository repo)
        {
            _repository = repo;
        }

        /// <summary>
        /// Добавить игру.
        /// </summary>
        public void Add(Game game)
        {
            _repository.Add(game);
        }
        /// <summary>
        /// Удалить игру.
        /// </summary>
        public void Delete(Game game)
        {
            _repository.Delete(game);
        }
        /// <summary>
        /// Получить все игры.
        /// </summary>
        public List<Game> ReadAll()
        {
            return _repository.ReadAll();
        }
        /// <summary>
        /// Получить игру по Id.
        /// </summary>
        public Game ReadById(int id)
        {
            return _repository.ReadById(id);
        }
        /// <summary>
        /// Обновить игру.
        /// </summary>
        public void Update(Game game)
        {
            _repository.Update(game);
        }
    }
}
