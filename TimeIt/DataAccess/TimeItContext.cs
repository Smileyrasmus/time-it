using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class TimeItContext : DbContext
    {
        public DbSet<TestObject> TestObjects { get; set; }

        public string DbPath { get; set; }

        public TimeItContext()
        {
            DbPath = GetDbPath();
        }

        public TimeItContext(string? dbName)
        {
            DbPath = GetDbPath(dbName);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        private string GetDbPath(string? dbName = null)
        {
            // get the folder from "DOTNET_SQLITE_DB_FOLDER" environment variable and check if the path is valid
            var path = Environment.GetEnvironmentVariable("DOTNET_SQLITE_DB_FOLDER");
            if (string.IsNullOrEmpty(path))
            {
                // if the environment variable is not set, use the default path
                var folder = Environment.SpecialFolder.LocalApplicationData;
                path = Environment.GetFolderPath(folder);
            }
            if (!System.IO.Directory.Exists(path))
            {
                // Throw error if the path is not valid
                throw new Exception($"The path {path} does not exist. This path is set by the environment variable DOTNET_SQLITE_DB_FOLDER");
            }

            return System.IO.Path.Join(path, dbName ?? "TimeIt.db");
        }
    }
}
