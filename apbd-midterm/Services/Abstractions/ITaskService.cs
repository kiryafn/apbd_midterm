using apbd_midterm.DTO;

namespace apbd_midterm.Services.Abstractions;

public interface ITaskService
{
    public Task<MemberTasksDTO> GetMemberTasksAsync(int memberId);
    public Task<int> CreateTaskAsync(TaskCreationRequest task);
}