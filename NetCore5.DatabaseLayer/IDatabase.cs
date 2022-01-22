using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore5.DatabaseLayer
{
    public interface IDatabase
    {
        public string ConnectionString { get; set; }
        public DataSet GetDataset(string CommandText);
        public int ExecuteNonQuery(string CommandText);
    }
}
