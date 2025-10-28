using GameStore.Entity;

namespace GameStore.DataAccessLayer
{
    /// <summary>
    /// Определяет операции для работы с хранилищем игр.
    /// </summary>
    public interface IGameRepository : IRepository<Game>
    {
    }
}
