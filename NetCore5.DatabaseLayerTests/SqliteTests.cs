using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Extensions.Logging;

namespace NetCore5.DatabaseLayer.Tests
{
    [TestClass()]
    public class SqliteTests
    {
        static ILoggerFactory _loggerFactory = (ILoggerFactory)new LoggerFactory();
        static ILogger<SqliteTests> logger = _loggerFactory.CreateLogger<SqliteTests>();
        static int result;
        static IDatabase Database;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            //use database master
            string connString = configuration.GetConnectionString("sqLiteConnection");
            Database = new Sqlite(connString, logger);
        }

        [TestMethod]
        public void T0_TestInsert()
        {
            int result = Database.ExecuteNonQuery("insert into result(testName) values('test')");
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void T1_TestGetDataSet()
        {
            DataSet ds = Database.GetDataset("select * from result");
            Assert.IsTrue(ds.Tables.Count > 0);
            Assert.IsTrue(ds.Tables[0].Rows.Count > 0);
        }

        [TestMethod]
        public void T2_TestDelete()
        {
            result = Database.ExecuteNonQuery("DELETE from result where testName='test'");
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void T2_TestExecuteScalar()
        {
            var result = Database.ExecuteScalar("select count(*) from result");
            Assert.IsTrue(int.TryParse(result.ToString(), out int i));
        }
    }
}