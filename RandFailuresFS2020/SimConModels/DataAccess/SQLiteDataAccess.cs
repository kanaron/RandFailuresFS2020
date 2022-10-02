using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;

namespace SimConModels
{
    public class SQLiteDataAccess
    {
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

        /*public static string LoadFirstValue(string select, string from, string where)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var query = cnn.QueryFirst<string>($"SELECT { select } FROM { from } WHERE { where} ").ToString();
                if (query != null)
                {
                    return query;
                }
                return "";
            }
        }*/

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
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
