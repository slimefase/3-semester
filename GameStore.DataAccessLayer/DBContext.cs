using Microsoft.EntityFrameworkCore;
using GameStore.Entity;

namespace GameStore.DataAccessLayer
{
    public class DbContextGameStore : DbContext
    {
        public DbSet<Game> Games { get; set; }

        public DbContextGameStore()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\..\GameStore.DataAccessLayer"));
            string dbPath = Path.Combine(projectRoot, "games.mdf");

            optionsBuilder.UseSqlServer($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};Initial Catalog=GameStoreDB;Integrated Security=True");
        }
    }
}
