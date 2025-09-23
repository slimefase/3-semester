using GameStore.DataAccessLayer;
using GameStore.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Windows.Forms;

namespace GameStore.WinFormsApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            string dbPath = @"C:\Учёба\Архитектура информационных систем\3-semester\GameStoreDatabase.mdf";

            var connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};Initial Catalog=MySharedGameStoreDB;Integrated Security=True;Connect Timeout=30";

            var options = new DbContextOptionsBuilder<DBContext>()
                .UseSqlServer(connectionString)
                .Options;

            using (var context = new DBContext(options))
            {
                try
                {
                    context.Database.EnsureCreated();
                }
                catch (SqlException ex)
                {
                    if (!ex.Message.Contains("уже существует") && !ex.Message.Contains("already exists"))
                    {
                        throw;
                    }
                }
            }

            IRepository<Game> repository = new EntityRepository<Game>(new DBContext(options));

            var gameLogic = new Logic(repository);
            Application.Run(new Form1(gameLogic));
        }
    }
}
