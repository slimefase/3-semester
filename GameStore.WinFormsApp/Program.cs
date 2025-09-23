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

            // Получаем корневую папку решения (3-semester) относительно текущей директории
            string solutionRoot = GetSolutionRootDirectory();

            // Формируем путь к .mdf файлу базы данных
            string dbPath = Path.GetFullPath(Path.Combine(solutionRoot, "GameStoreDatabase.mdf"));

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

        /// <summary>
        /// Метод для определения корневой директории решения (папки 3-semester).
        /// Предполагается, что приложение запускается внутри структуры решения.
        /// </summary>
        /// <returns>Полный путь к корню решения</returns>
        private static string GetSolutionRootDirectory()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var directory = new DirectoryInfo(currentDirectory);
            while (directory != null && directory.Name.ToLower() != "3-semester")
            {
                directory = directory.Parent;
            }
            if (directory == null)
                throw new DirectoryNotFoundException("Корневая папка решения '3-semester' не найдена.");
            return directory.FullName;
        }
    }
}
