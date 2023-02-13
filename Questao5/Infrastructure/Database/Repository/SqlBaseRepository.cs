using Microsoft.Data.Sqlite;

namespace Questao5.Infrastructure.Database.Repository
{
    public class SqlBaseRepository
    {
        public static string DbFile
        {
            get { return Environment.CurrentDirectory + "\\database.sqlite"; }
        }

        public static SqliteConnection SimpleDbConnection()
        {
            return new SqliteConnection("Data Source=" + DbFile);
        }
    }
}
