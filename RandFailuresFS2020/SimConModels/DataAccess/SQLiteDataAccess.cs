using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace SimConModels
{
    public class SQLiteDataAccess
    {
        protected static readonly string databaseName = "FailDB.db";

        public static void Update(string sql)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(sql);
            }
        }

        public static void Insert(string sql)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(sql);
            }
        }

        public static string LoadFirstValue(string sql)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var query = cnn.QueryFirst<string>(sql).ToString();
                if (query != null)
                {
                    return query;
                }
                return "";
            }
        }

        /// <summary>
        /// Takes connection string for database
        /// </summary>
        /// <param name="id">
        /// which connection string get
        /// </param>
        /// <returns>
        /// connection string
        /// </returns>
        public static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString
                .Replace("%databasePath%", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RandFailures", "Database", databaseName));
        }
    }
}
