using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RijlesPlanner.DataAccessLayer.Connection
{
    public static class ConnectionString
    {
        private static readonly string connectionString = "Server=127.0.0.1;Database=RijlesPlanner;User Id=SA;Password=H3ll0-ry4n";

        public static string GetConnectionString()
        {
            return connectionString;
        }
    }
}
