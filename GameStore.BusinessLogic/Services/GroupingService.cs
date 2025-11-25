using GameStore.Entity;
using GameStore.DataAccessLayer;
using GameStore.BusinessLogic.Interfaces;

namespace GameStore.BusinessLogic.Services
{
    public class GroupingService : IGroupingService
    {
        private readonly IGameRepository _repository;

        /// <summary>
        /// Внедряет репозиторий для работы с играми
        /// </summary>
        public GroupingService(IGameRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Группирует игры по жанрам
        /// </summary>
        public Dictionary<string, List<Game>> GroupByGenre()
        {
            return _repository.ReadAll()
                              .GroupBy(g => g.Genre)
                              .ToDictionary(g => g.Key, g => g.ToList());
        }
    }
}
