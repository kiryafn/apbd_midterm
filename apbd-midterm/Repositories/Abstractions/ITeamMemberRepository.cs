using apbd_midterm.Models;

namespace apbd_midterm.Repositories.Abstractions;

public interface ITeamMemberRepository
{
    public Task<TeamMember?> GetByIdAsync(int memberId);
}