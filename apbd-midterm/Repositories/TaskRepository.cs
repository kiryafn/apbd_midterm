using apbd_midterm.Repositories.Abstractions;
using Microsoft.Data.SqlClient;
using Task = apbd_midterm.Models.Task;

namespace apbd_midterm.Repositories;

public class TaskRepository : BaseRepository, ITaskRepository
{
    public TaskRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<List<Task>?> GetAllByMemberIdAsync(int memberId)
    {
        var tasks = new List<Task>();
        
        const string sql = "SELECT * FROM Task WHERE IdAssignedTo = @memberId ORDER BY Deadline DESC";
        
        await using var connection = GetConnection();
        await connection.OpenAsync();
        await using var cmd = new SqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@memberId", memberId);
        await using var reader = await cmd.ExecuteReaderAsync();
        
        if (!reader.HasRows)
        {
            return null;
        }

        while (reader.Read())
        {
            var task = new Task();
            task.IdTask = (int)reader["IdTask"];
            task.Name = (string)reader["Name"];
            task.Description = (string)reader["Description"];
            task.Deadline = (DateTime)reader["Deadline"];
            task.IdProject = (int)reader["IdTask"];
            task.IdTaskType = (int)reader["IdTaskType"];
            task.IdAssignedTo = (int)reader["IdAssignedTo"];
            task.IdCreator = (int)reader["IdCreator"];
            tasks.Add(task);
        }
        
        return tasks;
    }

    public async Task<List<Task>?> GetAllByCreatorIdAsync(int creatorId)
    {
        var tasks = new List<Task>();
        
        const string sql = "SELECT * FROM Task WHERE IdCreator = @creatorId ORDER BY Deadline DESC";
        
        await using var connection = GetConnection();
        await connection.OpenAsync();
        await using var cmd = new SqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@creatorId", creatorId);
        await using var reader = await cmd.ExecuteReaderAsync();
        
        if (!reader.HasRows)
        {
            return null;
        }

        while (reader.Read())
        {
            var task = new Task();
            task.IdTask = (int)reader["IdTask"];
            task.Name = (string)reader["Name"];
            task.Description = (string)reader["Description"];
            task.Deadline = (DateTime)reader["Deadline"];
            task.IdProject = (int)reader["IdTask"];
            task.IdTaskType = (int)reader["IdTaskType"];
            task.IdAssignedTo = (int)reader["IdAssignedTo"];
            task.IdCreator = (int)reader["IdCreator"];
            tasks.Add(task);
            
        }
        
        return tasks;
    }


    public async Task<Task> CreateTask(Task task)
    {
        const string sql = @"
        INSERT INTO Tasks (Name, Description, Deadline, IdProject, IdTaskType, IdAssignedTo, IdCreator)
        VALUES (@Name, @Description, @Deadline, @IdProject, @IdTaskType, @IdAssignedTo, @IdCreator)
        SELECT CAST(SCOPE_IDENTITY());";

        await using var con = GetConnection();
        await using var cmd = new SqlCommand(sql, con);
        await con.OpenAsync();
        cmd.Parameters.AddWithValue("@Name", task.Name);
        cmd.Parameters.AddWithValue("@Description", task.Description);
        cmd.Parameters.AddWithValue("@Deadline", task.Deadline);
        cmd.Parameters.AddWithValue("@IdProject", task.IdProject);
        cmd.Parameters.AddWithValue("@IdTaskType", task.IdTaskType);
        cmd.Parameters.AddWithValue("@IdAssignedTo", task.IdAssignedTo);
        cmd.Parameters.AddWithValue("@IdCreator", task.IdCreator);

        var result = await cmd.ExecuteScalarAsync();
        task.IdTaskType =  Convert.ToInt32(result);
        return task;
    }
}