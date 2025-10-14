using GameStore.Entity;
using System.Collections.Generic;

namespace GameStore.DataAccessLayer
{
    /// <summary>
    /// Определяет базовые операции для работы с хранилищем данных игр.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Добавляет новую игру в хранилище.
        /// </summary>
        /// <param name="game">Объект игры для добавления.</param>
        void Add(Game game);

        /// <summary>
        /// Удаляет игру из хранилища.
        /// </summary>
        /// <param name="game">Объект игры для удаления.</param>
        void Delete(Game game);

        /// <summary>
        /// Возвращает все игры из хранилища.
        /// </summary>
        /// <returns>Список всех игр.</returns>
        List<Game> ReadAll();

        /// <summary>
        /// Возвращает игру по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор игры.</param>
        /// <returns>Объект игры, если найден, иначе null.</returns>
        Game ReadById(int id);

        /// <summary>
        /// Обновляет данные существующей игры.
        /// </summary>
        /// <param name="game">Объект игры с обновлёнными данными.</param>
        void Update(Game game);
    }
}
