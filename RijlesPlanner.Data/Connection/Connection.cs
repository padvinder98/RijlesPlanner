using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using RijlesPlanner.IData.Interfaces.ConnectionFactory;

namespace RijlesPlanner.Data.Connection
{
    public class Connection : IConnection
    {
        private readonly string connectionString = ConnectionString.GetConnectionString();

        public IDbConnection GetConnection
        {
            get
            {
                var conn = new SqlConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
                return conn;
            }
        }
    }
}
