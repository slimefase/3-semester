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
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var dir = new DirectoryInfo(baseDir);

            DirectoryInfo? found = null;
            while (dir != null)
            {
                var candidate = Path.Combine(dir.FullName, "GameStore.DataAccessLayer");
                if (Directory.Exists(candidate))
                {
                    found = new DirectoryInfo(candidate);
                    break;
                }
                dir = dir.Parent;
            }

            string dbFolder;
            if (found != null)
            {
                dbFolder = found.FullName;
            }
            else
            {
                // fallback — тот путь, который ты указал как желаемый
                dbFolder = @"C:\Учёба\Архитектура информационных систем\3-semester\GameStore.DataAccessLayer";
            }

            Directory.CreateDirectory(dbFolder); // убедиться, что папка есть
            string dbPath = Path.Combine(dbFolder, "games.mdf");

            optionsBuilder.UseSqlServer($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};Initial Catalog=GameStoreDB;Integrated Security=True;Connect Timeout=30");
        }

    }
}

