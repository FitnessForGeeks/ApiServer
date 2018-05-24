using System;
using MySql.Data.MySqlClient;

namespace FitnessForGeeksWebApi.Database
{
    public static class MySqlDatabase
    {
        private static string url, database, username, password;
        private static int port;

        public static void init(string url, int port, string database, string username, string password)
        {
            MySqlDatabase.url = url;
            MySqlDatabase.port = port;
            MySqlDatabase.database = database;
            MySqlDatabase.username = username;
            MySqlDatabase.password = password;
        }

        public static MySqlConnection CreateConnection()
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server = url,
                Database = database,
                Password = password,
                UserID = username,
                SslMode = MySqlSslMode.None
            };

            var conn = new MySqlConnection(connectionStringBuilder.ToString());
            conn.Open();

            return conn;
        }

        public static Nullable<T> GetValueOrNull<T>(MySqlDataReader reader, string columnName) where T : struct
        {
            object columnValue = reader[columnName];

            if (!(columnValue is DBNull))
                return (T)columnValue;

            return null;
        }

        public static T GetValue<T>(MySqlDataReader reader, string columnName) where T : class
        {
            object value = reader[columnName];
            return value is DBNull
                ? null
                : (T) value;
        }

        public static void ExecuteNoneQuery(string query, Action<int> cb)
        {
            using (var conn = CreateConnection())
            using (var command = new MySqlCommand())
            {
                command.Connection = conn;
                command.CommandText = query;
                var rowCount = command.ExecuteNonQuery();
                cb.Invoke(rowCount);
            }
        }

        public static void ExecuteReader(string query, Action<MySqlDataReader> cb, bool resultIsRequired = false)
        {
            using (var conn = CreateConnection())
            using (var command = new MySqlCommand())
            {
                command.Connection = conn;
                command.CommandText = query;
                var reader = command.ExecuteReader();
                if(!reader.HasRows && resultIsRequired)
                {
                    throw new ResultIsEmptyException();
                }
                while (reader.Read())
                {
                    cb(reader);
                }
            }
        }
    }
}
