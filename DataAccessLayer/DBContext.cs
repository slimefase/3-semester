using GameStore.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace GameStore.DataAccessLayer
{
    /// <summary>
    /// Контекст базы данных для работы с игровым магазином.
    /// </summary>
    public class DBContext : DbContext
    {
        /// <summary>
        /// Набор игр в базе данных.
        /// </summary>
        public DbSet<Game> Games { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр контекста базы данных.
        /// </summary>
        /// <param name="options">Параметры конфигурации контекста</param>
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        /// <summary>
        /// Конфигурирует подключение к базе данных.
        /// </summary>
        /// <param name="optionsBuilder">Строитель параметров контекста</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\GameStoreDatabase.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        /// <summary>
        /// Конфигурирует модель данных.
        /// </summary>
        /// <param name="modelBuilder">Строитель модели данных</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Genre).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(5,2)");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
