using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace CrudApp.WebAPI.Helper
{
    public static class DatabaseHelper
    {
        //This is field because its as class level
        private static string DbFileName = "Actors.db";

        //This is field because its as class level
        private static string ConnectionString = $"Data Source={DbFileName};";

        public static void EnsureDatabase()
        {
            if (!File.Exists(DbFileName))
            {
                using (var connection = new SqliteConnection(ConnectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    command.CommandText = @"CREATE TABLE IF NOT EXISTS Actors (
                                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                Name TEXT NOT NULL,
                                                Description TEXT NOT NULL,
                                                Age INTEGER NOT NULL,
                                                NetWorth INTEGER NOT NULL
                                              );";
                    command.ExecuteNonQuery();
                }

            }
        }
        public static string GetConnectionString()
        {
            return ConnectionString;
        }

    }
}