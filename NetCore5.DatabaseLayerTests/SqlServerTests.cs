using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Extensions.Logging;

namespace NetCore5.DatabaseLayer.Tests
{
    [TestClass()]
    public class SqlServerTests
    {
        static IDatabase testDatabase;
        static int result;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            string createDbScript = System.IO.File.ReadAllText("Scripts\\CreateDatabase.sql");
            string createTableScript = System.IO.File.ReadAllText("Scripts\\CreateTable.sql");

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            //use database master
            string connString = configuration.GetConnectionString("DefaultConnection");
            IDatabase masterDatabase = new SqlServer(connString);
            result = masterDatabase.ExecuteNonQuery(createDbScript);

            //switch database to test
            connString = configuration.GetConnectionString("testconnection");
            testDatabase = new SqlServer(connString);
            result = testDatabase.ExecuteNonQuery(createTableScript);
        }

        [TestMethod()]
        public void T0_TestGetDatasetTest()
        {
            result = testDatabase.ExecuteNonQuery("delete a");
            result = testDatabase.ExecuteNonQuery("insert into a(b) values(1),(2),(3)");
            DataSet ds = testDatabase.GetDataset("select * from a");
            Assert.IsTrue(ds.Tables.Count > 0);
            Assert.IsTrue(ds.Tables[0].Rows.Count > 0);
        }

        [TestMethod]
        public void T1_TestExecuteScalar()
        {
            var result = testDatabase.ExecuteScalar("select count(*) from a");
            Assert.IsTrue(int.TryParse(result.ToString(), out int i));
        }

        [TestMethod]
        public void T2_DropTable()
        {
            result = testDatabase.ExecuteNonQuery("drop table a");
        }
    }
}