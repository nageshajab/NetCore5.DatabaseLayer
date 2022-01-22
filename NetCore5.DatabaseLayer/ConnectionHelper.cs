using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore5.DatabaseLayer
{
    public class ConnectionHelper
    {
        public static DbConnection GetDbConnection(string connectionString,
            DbConnectionType type)
        {
            switch (type)
            {
                case DbConnectionType.Sqlite:
                    return new SqliteConnection(connectionString);

                case DbConnectionType.SqlServer:
                    return new SqlConnection(connectionString);

                default:
                    return new SqlConnection(connectionString);
            };
        }

        public static void OpenConnection(DbConnection dbConnection)
        {
            if (dbConnection.State != System.Data.ConnectionState.Open)
            {
                dbConnection.Open();
            }
        }

        public static void CloseConnection(DbConnection dbConnection)
        {
            if (dbConnection.State == System.Data.ConnectionState.Open)
                dbConnection.Close();

            dbConnection.Dispose();
        }
    }
}
