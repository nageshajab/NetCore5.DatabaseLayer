using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using NLog.Web;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Extensions.Logging;

namespace NetCore5.DatabaseLayer.Tests
{
    [TestClass()]
    public class SqliteTests
    {
        static ILoggerFactory _loggerFactory = (ILoggerFactory)new LoggerFactory();
        ILogger<SqliteTests> logger = _loggerFactory.CreateLogger<SqliteTests>();

        [TestMethod()]
        public void GetDatasetTest()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            //use database master
            string connString = configuration.GetConnectionString("sqLiteConnection");
            IDatabase Database = new Sqlite(connString, logger);

            int result = Database.ExecuteNonQuery("insert into result(testName) values('test')");
            Assert.IsTrue(result > 0);

            DataSet ds = Database.GetDataset("select * from result");
            Assert.IsTrue(ds.Tables.Count > 0);
            Assert.IsTrue(ds.Tables[0].Rows.Count > 0);

            result = Database.ExecuteNonQuery("DELETE from result where testName='test'");
            Assert.IsTrue(result > 0);
        }
    }
}