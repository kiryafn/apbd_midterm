using apbd_midterm.Repositories.Abstractions;
using Microsoft.Data.SqlClient;

namespace apbd_midterm.Repositories;

public class TaskTypeRepository : BaseRepository, ITaskTypeRepository
{
    public TaskTypeRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<string?> GetNameByIdAsync(int taskTypeId)
    {
        const string sql = "SELECT Name FROM TaskType WHERE IdTaskType = @taskTypeId";
        
        await using var connection = GetConnection();
        await connection.OpenAsync();
        await using var cmd = new SqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@taskTypeId", taskTypeId);
        
        var result = await cmd.ExecuteScalarAsync();
        return result as string;


    }
}