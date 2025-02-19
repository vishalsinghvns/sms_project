using System.Configuration;
using System.Data.SqlClient;

public class Connection
{
    protected readonly string connectionString;

    public Connection()
    {
        connectionString = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;
    }

    public SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }
}
