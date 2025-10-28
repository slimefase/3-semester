using GameStore.Entity;

namespace GameStore.BusinessLogic.Interfaces
{
    public interface IGroupingService
    {
        Dictionary<string, List<Game>> GroupByGenre();
    }
}
