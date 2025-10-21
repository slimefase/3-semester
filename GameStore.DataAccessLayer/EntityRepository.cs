using GameStore.Entity;

namespace GameStore.DataAccessLayer
{
    /// <summary>
    /// Реализация репозитория игр на основе Entity Framework Core.
    /// </summary>
    public class EntityRepository : IGameRepository
    {
        private readonly DbContextGameStore _context;

        /// <summary>
        /// Инициализирует репозиторий и обеспечивает создание базы данных.
        /// </summary>
        public EntityRepository()
        {
            _context = new DbContextGameStore();
            _context.Database.EnsureCreated();
        }

        /// <summary>
        /// Добавляет игру в базу данных.
        /// </summary>
        /// <param name="game">Объект игры для добавления.</param>
        public void Add(Game game)
        {
            _context.Games.Add(game);
            _context.SaveChanges();
        }

        /// <summary>
        /// Удаляет игру из базы данных.
        /// </summary>
        /// <param name="game">Объект игры для удаления.</param>
        public void Delete(Game game)
        {
            _context.Games.Remove(game);
            _context.SaveChanges();
        }

        /// <summary>
        /// Возвращает список всех игр.
        /// </summary>
        /// <returns>Список игр из базы данных.</returns>
        public List<Game> ReadAll()
        {
            return _context.Games.ToList();
        }

        /// <summary>
        /// Находит игру по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор игры.</param>
        /// <returns>Найденная игра или null, если не найдена.</returns>
        public Game ReadById(int id)
        {
            return _context.Games.Find(id);
        }

        /// <summary>
        /// Обновляет данные существующей игры.
        /// </summary>
        /// <param name="game">Объект игры с обновлёнными данными.</param>
        public void Update(Game game)
        {
            _context.Games.Update(game);
            _context.SaveChanges();
        }
    }
}
