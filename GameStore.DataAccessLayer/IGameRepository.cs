using GameStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataAccessLayer
{
    /// <summary>
    /// Определяет операции для работы с хранилищем игр.
    /// </summary>
    public interface IGameRepository : IRepository<Game>
    {
    }
}
