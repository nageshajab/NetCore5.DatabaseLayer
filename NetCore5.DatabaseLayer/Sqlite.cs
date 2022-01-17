using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore5.DatabaseLayer
{
    public class Sqlite : IDatabase
    {
        public string ConnectionString { get; set; }
        public ILogger logger;
//        private readonly SqliteConnection sqliteConnection;

        public Sqlite(string _ConnectionString, ILogger _logger)
        {
            ConnectionString = _ConnectionString;
            logger = _logger;
  //          sqliteConnection = new SqliteConnection(ConnectionString);
        }

        private void OpenConnection()
        {
            //if (sqliteConnection.State != ConnectionState.Open)
            //{
            //    sqliteConnection.ConnectionString = ConnectionString;
            //    sqliteConnection.Open();
            //}
        }

        public void CloseConnection()
        {
            //if (sqliteConnection.State == ConnectionState.Open)
            //    sqliteConnection.Close();

            //sqliteConnection.Dispose();
        }

        public int ExecuteNonQuery(string CommandText)
        {
            try
            {
                OpenConnection();

                //using SqliteCommand command = sqliteConnection.CreateCommand();
                //command.CommandText = CommandText;
                //int result = command.ExecuteNonQuery();
                return 0;// result;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public DataSet GetDataset(string CommandText)
        {
            DataSet dataSet = new();
            DataTable dt = new();
            try
            {
                //sqliteConnection.Open();
                //SqliteCommand command = sqliteConnection.CreateCommand();
                //command.CommandText = CommandText;

                //using (var reader = command.ExecuteReader())
                //{
                //    dt.Load(reader);
                //}
                //dataSet.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return dataSet;
        }
    }
}
