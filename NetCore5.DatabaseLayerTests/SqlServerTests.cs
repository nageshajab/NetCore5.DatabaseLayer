using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCore5.DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.Web;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Extensions.Logging;

namespace NetCore5.DatabaseLayer.Tests
{
    [TestClass()]
    public class SqlServerTests
    {
        static ILoggerFactory _loggerFactory = (ILoggerFactory)new LoggerFactory();
        ILogger<SqliteTests> logger = _loggerFactory.CreateLogger<SqliteTests>();

        [TestMethod()]
        public void GetDatasetTest()
        {
            string createDbScript = System.IO.File.ReadAllText("Scripts\\CreateDatabase.sql");
            string createTableScript = System.IO.File.ReadAllText("Scripts\\CreateTable.sql");

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            //use database master
            string connString = configuration.GetConnectionString("DefaultConnection");
            IDatabase masterDatabase = new SqlServer(connString, logger);
            int result = masterDatabase.ExecuteNonQuery(createDbScript);

            //switch database to test
            connString = configuration.GetConnectionString("testconnection");
            IDatabase testDatabase = new SqlServer(connString, logger);

            result = testDatabase.ExecuteNonQuery(createTableScript);
            result = testDatabase.ExecuteNonQuery("delete a");
            result = testDatabase.ExecuteNonQuery("insert into a(b) values(1),(2),(3)");
            DataSet ds = testDatabase.GetDataset("select * from a");
            result = testDatabase.ExecuteNonQuery("drop table a");
            Assert.IsTrue(ds.Tables.Count > 0);
            Assert.IsTrue(ds.Tables[0].Rows.Count > 0);

        }
    }
}