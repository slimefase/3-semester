using GameStore.Entity;

namespace GameStore.BusinessLogic.Interfaces
{
    public interface IDiscountService
    {
        List<Game> GetDiscountedGames();
    }
}
