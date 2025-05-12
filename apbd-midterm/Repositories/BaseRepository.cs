using Microsoft.Data.SqlClient;

namespace apbd_midterm.Repositories;

public class BaseRepository
{
    private readonly string _connectionString;
    
    public BaseRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");    
    }

    protected SqlConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }
}