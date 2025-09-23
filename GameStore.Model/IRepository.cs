using System.Collections.Generic;
using GameStore.Model;

namespace GameStore.Model
{
    /// <summary>
    /// Интерфейс репозитория для работы с доменными объектами.
    /// </summary>
    /// <typeparam name="T">Тип доменного объекта</typeparam>
    public interface IRepository<T> where T : IDomainObject
    {
        T Add(T entity);
        void Delete(int id);
        IEnumerable<T> ReadAll();
        T ReadById(int id);
        void Update(T entity);
    }
}
