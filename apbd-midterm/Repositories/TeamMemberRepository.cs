using apbd_midterm.Models;
using apbd_midterm.Repositories.Abstractions;
using Microsoft.Data.SqlClient;

namespace apbd_midterm.Repositories;

public class TeamMemberRepository : BaseRepository, ITeamMemberRepository
{
    public TeamMemberRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<TeamMember?> GetByIdAsync(int memberId)
    {
        var teamMember = new TeamMember();
        
        const string sql = "SELECT * FROM TeamMember WHERE IdTeamMember = @memberId";
        
        await using var connection = GetConnection();
        await connection.OpenAsync();
        await using var cmd = new SqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@memberId", memberId);
        await using var reader = await cmd.ExecuteReaderAsync();
        
        if (!reader.HasRows)
        {
            return null;
        }

        if (reader.Read())
        {
            teamMember.IdTeamMember = (int)reader["IdTeamMember"];
            teamMember.FirstName = (string)reader["FirstName"];
            teamMember.LastName = (string)reader["LastName"];
            teamMember.Email = (string)reader["Email"];
            return teamMember;
        }
        
        return null;
    }
}