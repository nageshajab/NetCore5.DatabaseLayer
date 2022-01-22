using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Microsoft.Data.Sqlite;
using NLog;
using Microsoft.Extensions.Logging;

namespace NetCore5.DatabaseLayer
{
    /// <summary>
    /// Contains common methods which developer frequently needs while communicating with Sql Server database
    /// </summary>
    public class Sqlite : IDatabase
    {
        private readonly SqliteConnection sqlConnection;
        private Microsoft.Extensions.Logging.ILogger _logger;
        public string ConnectionString { get; set; }

        public Sqlite(string _ConnectionString, Microsoft.Extensions.Logging.ILogger logger)
        {
            ConnectionString = _ConnectionString;
            sqlConnection = (SqliteConnection)ConnectionHelper.GetDbConnection(_ConnectionString, DbConnectionType.Sqlite);
            _logger = logger;
        }


        /// <summary>
        /// Returns dataset from command text
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <param name="CommandText"></param>
        /// <returns></returns>
        public DataSet GetDataset(string CommandText)
        {
            DataSet dataSet;
            try
            {
                ConnectionHelper.OpenConnection(sqlConnection);

                dataSet = new();
                DataTable dt = new DataTable();

                using SqliteCommand command = new();
                command.Connection = sqlConnection;
                command.CommandText = CommandText;
                SqliteDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                dataSet.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.InnerException?.Message);
                throw;
            }
            finally
            {
                ConnectionHelper.CloseConnection(sqlConnection);
            }

            return dataSet;
        }

        /// <summary>
        /// Use this method for insert, update, delete operation
        /// </summary>
        /// <param name="CommandText"></param>
        /// <returns>number of rows affected</returns>
        public int ExecuteNonQuery(string CommandText)
        {
            try
            {
                ConnectionHelper.OpenConnection(sqlConnection);

                using SqliteCommand command = new();
                command.Connection = sqlConnection;
                command.CommandText = CommandText;
                int result = command.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.InnerException?.Message);
                throw;
            }
            finally
            {
                ConnectionHelper.CloseConnection(sqlConnection);
            }
        }
    }
}
