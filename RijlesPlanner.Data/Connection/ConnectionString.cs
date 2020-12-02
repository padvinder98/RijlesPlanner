namespace RijlesPlanner.Data.Connection
{
    public static class ConnectionString
    {
        private static readonly string connectionString = "Server=199.247.30.106;Database=RijlesPlanner;User Id=SA;Password=H3ll0-ry4n";

        public static string GetConnectionString()
        {
            return connectionString;
        }
    }
}
