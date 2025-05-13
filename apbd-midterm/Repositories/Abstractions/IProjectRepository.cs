namespace apbd_midterm.Repositories.Abstractions;

public interface IProjectRepository
{
    public Task<string?> GetNameByIdAsync(int projectId);
}