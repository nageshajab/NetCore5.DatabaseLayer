using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore5.DatabaseLayer.SQLite
{
    public class ParameterHelper
    {
        public static DbParameter GetParameter(string parameterName, object value,
            DbConnectionType databaseType)
        {
            switch (databaseType)
            {
                case DbConnectionType.Sqlite:
                    var sqliteParameter = new SqliteParameter
                    {
                        ParameterName = parameterName,
                        Value = value
                    };
                    return sqliteParameter;

                case DbConnectionType.SqlServer:
                    var sqlserverParameter = new SqlParameter
                    {
                        ParameterName = parameterName,
                        Value = value
                    };
                    return sqlserverParameter;

                default:
                    return null;
            }
        }
    }
}
