using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using GameStore.Model;

namespace GameStore.DataAccessLayer
{
    /// <summary>
    /// Реализация репозитория с использованием Dapper.
    /// </summary>
    /// <typeparam name="T">Тип доменного объекта</typeparam>
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        private readonly string _connectionString;
        private readonly string _tableName;

        /// <summary>
        /// Инициализирует новый экземпляр репозитория Dapper.
        /// </summary>
        /// <param name="connectionString">Строка подключения к базе данных</param>
        /// <param name="tableName">Имя таблицы в базе данных</param>
        public DapperRepository(string connectionString, string tableName)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _tableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
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

            using var connection = new SqlConnection(_connectionString);
            var sql = $"INSERT INTO {_tableName} (Title, Genre, Price, DiscountPercentage) VALUES (@Title, @Genre, @Price, @DiscountPercentage); SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = connection.Query<int>(sql, entity).Single();
            entity.Id = id;
            return entity;
        }

        /// <summary>
        /// Удаляет сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = $"DELETE FROM {_tableName} WHERE Id = @Id";
            connection.Execute(sql, new { Id = id });
        }

        /// <summary>
        /// Возвращает все сущности из базы данных.
        /// </summary>
        /// <returns>Коллекция всех сущностей</returns>
        public IEnumerable<T> ReadAll()
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = $"SELECT * FROM {_tableName}";
            return connection.Query<T>(sql);
        }

        /// <summary>
        /// Возвращает сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Найденная сущность или null</returns>
        public T ReadById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = $"SELECT * FROM {_tableName} WHERE Id = @Id";
            return connection.QuerySingleOrDefault<T>(sql, new { Id = id });
        }

        /// <summary>
        /// Обновляет существующую сущность.
        /// </summary>
        /// <param name="entity">Сущность для обновления</param>
        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            using var connection = new SqlConnection(_connectionString);
            var sql = $"UPDATE {_tableName} SET Title = @Title, Genre = @Genre, Price = @Price, DiscountPercentage = @DiscountPercentage WHERE Id = @Id";
            connection.Execute(sql, entity);
        }
    }
}
