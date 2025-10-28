using GameStore.Entity;
using GameStore.DataAccessLayer;
using GameStore.BusinessLogic.Interfaces;

namespace GameStore.BusinessLogic.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IGameRepository _repository;

        /// <summary>
        /// Внедряет репозиторий для работы с играми.
        /// </summary>
        public DiscountService(IGameRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Возвращает список игр, у которых есть скидка.
        /// </summary>
        public List<Game> GetDiscountedGames()
        {
            return _repository.ReadAll()
                              .Where(g => g.DiscountPercentage > 0)
                              .ToList();
        }
    }
}
