using Microsoft.Data.SqlClient;
using Dapper;
using GameStore.Entity;

namespace GameStore.DataAccessLayer
{
    /// <summary>
    /// Репозиторий для работы с играми через Dapper
    /// </summary>
    public class DapperRepository : IRepository<Game>
    {
        private readonly string _connectionString = @"Server=(localdb)\mssqllocaldb;Database=GameStoreDB;Trusted_Connection=True;";

        /// <summary>
        /// Добавляет новую игру в базу данных
        /// </summary>
        /// <param name="entity">Игра для добавления</param>
        public void Add(Game entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("INSERT INTO Games (Title, Genre, Price, DiscountPercentage) VALUES (@Title, @Genre, @Price, @DiscountPercentage)", entity);
            }
        }

        /// <summary>
        /// Удаляет игру из базы данных
        /// </summary>
        /// <param name="entity">Игра для удаления</param>
        public void Delete(Game entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("DELETE FROM Games WHERE Id = @Id", new { entity.Id });
            }
        }

        /// <summary>
        /// Получает все игры из базы данных
        /// </summary>
        /// <returns>Список всех игр</returns>
        public List<Game> ReadAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Game>("SELECT * FROM Games").ToList();
            }
        }

        /// <summary>
        /// Получает игру по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор игры</param>
        /// <returns>Найденная игра или null</returns>
        public Game ReadById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Game>("SELECT * FROM Games WHERE Id = @Id", new { Id = id });
            }
        }

        /// <summary>
        /// Обновляет информацию об игре
        /// </summary>
        /// <param name="entity">Игра для обновления</param>
        public void Update(Game entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("UPDATE Games SET Title = @Title, Genre = @Genre, Price = @Price, DiscountPercentage = @DiscountPercentage WHERE Id = @Id", entity);
            }
        }
    }
}
