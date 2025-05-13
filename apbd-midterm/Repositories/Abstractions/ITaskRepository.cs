namespace apbd_midterm.Repositories.Abstractions;
using Models;

public interface ITaskRepository
{
    public Task<List<Task>?> GetAllByMemberIdAsync(int memberId);
    public Task<List<Task>?> GetAllByCreatorIdAsync(int creatorId);
    public Task<Task> CreateTask(Task task);
}