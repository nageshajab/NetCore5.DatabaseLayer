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
            sqlConnection = new(ConnectionString);
            _logger = logger;
        }

        private void OpenConnection()
        {
            if (sqlConnection.State != ConnectionState.Open) {
                sqlConnection.ConnectionString = ConnectionString;
                sqlConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (sqlConnection.State == ConnectionState.Open)
                sqlConnection.Close();

            sqlConnection.Dispose();
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
                OpenConnection();

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
                CloseConnection();
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
                OpenConnection();

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
                CloseConnection();
            }
        }
    }
}
