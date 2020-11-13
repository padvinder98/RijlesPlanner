using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RijlesPlanner.DataAccessLayer.Connection
{
    public static class ConnectionString
    {
        private static readonly string connectionString = "";

        public static string GetConnectionString()
        {
            return connectionString;
        }
    }
}
