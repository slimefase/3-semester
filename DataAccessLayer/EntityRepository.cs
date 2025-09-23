using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Model;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DataAccessLayer
{
    /// <summary>
    /// Реализация репозитория с использованием Entity Framework.
    /// </summary>
    /// <typeparam name="T">Тип доменного объекта</typeparam>
    public class EntityRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        private readonly DBContext _context;
        private readonly DbSet<T> _dbSet;

        /// <summary>
        /// Инициализирует новый экземпляр репозитория Entity Framework.
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public EntityRepository(DBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        /// <summary>
        /// Добавляет новую сущность в базу данных.
        /// </summary>
        /// <param name="entity">Сущность для добавления</param>
        /// <returns>Добавленная сущность</returns>
        public T Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var added = _dbSet.Add(entity);
            _context.SaveChanges();
            return added.Entity;
        }

        /// <summary>
        /// Удаляет сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        public void Delete(int id)
        {
            var entity = ReadById(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Возвращает все сущности из базы данных.
        /// </summary>
        /// <returns>Коллекция всех сущностей</returns>
        public IEnumerable<T> ReadAll()
        {
            return _dbSet.ToList();
        }

        /// <summary>
        /// Возвращает сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Найденная сущность или null</returns>
        public T ReadById(int id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Обновляет существующую сущность.
        /// </summary>
        /// <param name="entity">Сущность для обновления</param>
        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
