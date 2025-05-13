namespace apbd_midterm.Repositories.Abstractions;

public interface ITaskTypeRepository
{
    public Task<string?> GetNameByIdAsync(int projectId);
}