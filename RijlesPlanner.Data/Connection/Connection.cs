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
                var connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();
                return connection;
            }
        }
    }
}
