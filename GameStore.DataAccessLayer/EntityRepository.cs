using GameStore.Model;

namespace GameStore.DataAccessLayer
{
    public class EntityRepository : IRepository
    {
        private readonly DbContextGameStore _context;

        public EntityRepository()
        {
            _context = new DbContextGameStore();
            _context.Database.EnsureCreated();
        }

        /// <summary>
        /// Добавить игру через EF.
        /// </summary>
        public void Add(Game game)
        {
            _context.Games.Add(game);
            _context.SaveChanges();
        }

        /// <summary>
        /// Удалить игру через EF.
        /// </summary>
        public void Delete(Game game)
        {
            _context.Games.Remove(game);
            _context.SaveChanges();
        }

        /// <summary>
        /// Получить все игры через EF.
        /// </summary>
        public List<Game> ReadAll()
        {
            return _context.Games.ToList();
        }

        /// <summary>
        /// Получить одну игру по Id через EF.
        /// </summary>
        public Game ReadById(int id)
        {
            return _context.Games.Find(id);
        }

        /// <summary>
        /// Обновить игру через EF.
        /// </summary>
        public void Update(Game game)
        {
            _context.Games.Update(game);
            _context.SaveChanges();
        }
    }
}
