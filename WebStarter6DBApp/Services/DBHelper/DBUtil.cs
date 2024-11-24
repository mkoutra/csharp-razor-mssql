using Microsoft.Data.SqlClient;

namespace WebStarter6DBApp.Services.DBHelper
{
    public static class DBUtil
    {
        // Returns a connection to SQL server from the connection pool.
        public static SqlConnection GetConnection()
        {
            SqlConnection connection;

            // Get connection string from appsettings.json
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            string url = configuration.GetConnectionString("DefaultConnection");

            try
            {
                connection = new SqlConnection(url);
                return connection;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
