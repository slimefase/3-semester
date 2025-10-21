using System.Collections.Generic;

namespace GameStore.DataAccessLayer
{
    /// <summary>
    /// Определяет базовые операции для работы с хранилищем данных.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Добавляет сущность в хранилище.
        /// </summary>
        /// <param name="entity">Добавляемая сущность.</param>
        void Add(T entity);

        /// <summary>
        /// Удаляет сущность из хранилища.
        /// </summary>
        /// <param name="entity">Удаляемая сущность.</param>
        void Delete(T entity);

        /// <summary>
        /// Возвращает все сущности из хранилища.
        /// </summary>
        /// <returns>Список всех сущностей.</returns>
        List<T> ReadAll();

        /// <summary>
        /// Возвращает сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Найденная сущность или null.</returns>
        T ReadById(int id);

        /// <summary>
        /// Обновляет данные сущности.
        /// </summary>
        /// <param name="entity">Сущность с обновлёнными данными.</param>
        void Update(T entity);
    }
}
