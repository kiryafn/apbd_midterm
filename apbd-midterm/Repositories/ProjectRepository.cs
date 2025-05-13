using apbd_midterm.Repositories.Abstractions;
using Microsoft.Data.SqlClient;

namespace apbd_midterm.Repositories;

public class ProjectRepository : BaseRepository, IProjectRepository
{
    public ProjectRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<string?> GetNameByIdAsync(int projectId)
    {
        const string sql = "SELECT Name FROM Project WHERE IdProject = @projectId";
        
        await using var connection = GetConnection();
        await connection.OpenAsync();
        await using var cmd = new SqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@projectId", projectId);
        
        var result = await cmd.ExecuteScalarAsync();
        return result as string;
    }
}