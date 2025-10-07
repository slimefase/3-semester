using GameStore.Model;
using System.Data.SQLite;
using Dapper;

namespace GameStore.DataAccessLayer
{
    public class DapperRepository : IRepository
    {
        private readonly string _connectionString = "Data Source=games.db";

        /// <summary>
        /// Инициализирует репозиторий и создает таблицу если необходимо.
        /// </summary>
        public DapperRepository()
        {
            InitializeDatabase();
        }

        /// <summary>
        /// Создает таблицу в базе данных если она не существует.
        /// </summary>
        private void InitializeDatabase()
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Execute(@"
                CREATE TABLE IF NOT EXISTS Games (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    Genre TEXT NOT NULL,
                    Price DECIMAL(18,2) NOT NULL,
                    DiscountPercentage DECIMAL(5,2) NOT NULL
                )");
        }

        /// <summary>
        /// Добавляет новую игру в базу данных через Dapper.
        /// </summary>
        public void Add(Game game)
        {
            using var connection = new SQLiteConnection(_connectionString);
            var sql = @"INSERT INTO Games (Title, Genre, Price, DiscountPercentage) 
                       VALUES (@Title, @Genre, @Price, @DiscountPercentage)";
            connection.Execute(sql, game);
        }

        /// <summary>
        /// Удаляет игру из базы данных через Dapper.
        /// </summary>
        public void Delete(Game game)
        {
            using var connection = new SQLiteConnection(_connectionString);
            var sql = "DELETE FROM Games WHERE Id = @Id";
            connection.Execute(sql, new { Id = game.Id });
        }

        /// <summary>
        /// Возвращает все игры из базы данных через Dapper.
        /// </summary>
        public List<Game> ReadAll()
        {
            using var connection = new SQLiteConnection(_connectionString);
            var sql = "SELECT * FROM Games";
            return connection.Query<Game>(sql).ToList();
        }

        /// <summary>
        /// Возвращает игру по ID через Dapper.
        /// </summary>
        public Game ReadById(int id)
        {
            using var connection = new SQLiteConnection(_connectionString);
            var sql = "SELECT * FROM Games WHERE Id = @Id";
            return connection.QueryFirstOrDefault<Game>(sql, new { Id = id });
        }

        /// <summary>
        /// Обновляет данные игры в базе данных через Dapper.
        /// </summary>
        public void Update(Game game)
        {
            using var connection = new SQLiteConnection(_connectionString);
            var sql = @"UPDATE Games 
                       SET Title = @Title, Genre = @Genre, Price = @Price, DiscountPercentage = @DiscountPercentage 
                       WHERE Id = @Id";
            connection.Execute(sql, game);
        }
    }
}