using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using NLog;
namespace NetCore5.DatabaseLayer
{
    /// <summary>
    /// Contains common methods which developer frequently needs while communicating with Sql Server database
    /// </summary>
    public class SqlServer : IDatabase
    {
        private readonly SqlConnection sqlConnection;
        private ILogger _logger;
        public string ConnectionString { get; set; }

        public SqlServer(string _ConnectionString, ILogger logger)
        {
            ConnectionString = _ConnectionString;
            sqlConnection = (SqlConnection)ConnectionHelper.GetDbConnection(_ConnectionString, DbConnectionType.SqlServer);
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

                SqlDataAdapter sqlDataAdapter = new();
                dataSet = new();
                using SqlCommand command = new();
                command.Connection = sqlConnection;
                command.CommandText = CommandText;
                sqlDataAdapter.SelectCommand = command;

                sqlDataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
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

                using SqlCommand command = new();
                command.Connection = sqlConnection;
                command.CommandText = CommandText;
                int result = command.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            finally
            {
                ConnectionHelper.CloseConnection(sqlConnection);
            }
        }
    }
}
