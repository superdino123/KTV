using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHelper
{
    public class ConnectionHelper
    {
        private static readonly string _connection =
            @"Data Source = {0};Initial Catalog = {1};User Id = {2}; Password = {3};";
        public static SqlConnection GetSqlConnection()
        {
            var sqlConnectionString =
                string.Format(_connection, "127.0.0.1", "KTVDB", "sa", "A1zhengweidi94");
            var conn = new SqlConnection(sqlConnectionString);
            return conn;
        }
    }
}
