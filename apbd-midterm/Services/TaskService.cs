using apbd_midterm.DTO;
using apbd_midterm.Exceptions;
using apbd_midterm.Repositories.Abstractions;
using apbd_midterm.Services.Abstractions;
using Task = apbd_midterm.Models.Task;


namespace apbd_midterm.Services;

public class TaskService : ITaskService
{
    public readonly ITaskRepository _taskRepository;
    public readonly IProjectRepository _projectRepository;
    public readonly ITaskTypeRepository _taskTypeRepository;
    public readonly ITeamMemberRepository _teamMemberRepository;
    
    public TaskService(ITaskRepository taskRepository, IProjectRepository projectRepository, ITaskTypeRepository taskTypeRepository, ITeamMemberRepository teamMemberRepository)
    {
        _taskRepository = taskRepository;
        _projectRepository = projectRepository;
        _taskTypeRepository = taskTypeRepository;
        _teamMemberRepository = teamMemberRepository;
    }
    
    
    
    public async Task<MemberTasksDTO> GetMemberTasksAsync(int memberId)
{
    var teamMember = await _teamMemberRepository.GetByIdAsync(memberId);
    if (teamMember == null)
    {
        throw new NotFoundException($"Team member with ID {memberId} not found.");
    }

    var assignedTasks = await _taskRepository.GetAllByMemberIdAsync(memberId);
    if (assignedTasks == null) Console.WriteLine("null");
    if (assignedTasks.Count == 0) Console.WriteLine("empty");

    var createdTasks = await _taskRepository.GetAllByCreatorIdAsync(memberId);

    var assignedTasksDTO = new List<TaskDTO>();
    
    if (assignedTasks != null)
    {
        foreach (var task in assignedTasks)
        {
            var projectName = await _projectRepository.GetNameByIdAsync(task.IdProject);
            var taskTypeName = await _taskTypeRepository.GetNameByIdAsync(task.IdTaskType);

            assignedTasksDTO.Add(new TaskDTO
            {
                IdTask = task.IdTask,
                Name = task.Name,
                Description = task.Description,
                Deadline = task.Deadline,
                ProjectName = projectName,
                TaskTypeName = taskTypeName
            });
        }
    }

    var createdTasksDTO = new List<TaskDTO>();
    if (createdTasks != null)
    {
        foreach (var task in createdTasks)
        {
            var projectName = await _projectRepository.GetNameByIdAsync(task.IdProject);
            var taskTypeName = await _taskTypeRepository.GetNameByIdAsync(task.IdTaskType);

            createdTasksDTO.Add(new TaskDTO
            {
                IdTask = task.IdTask,
                Name = task.Name,
                Description = task.Description,
                Deadline = task.Deadline,
                ProjectName = projectName,
                TaskTypeName = taskTypeName
            });
        }
    }

    var memberTasksDTO = new MemberTasksDTO
    {
        IdTeamMember = teamMember.IdTeamMember,
        FirstName = teamMember.FirstName,
        LastName = teamMember.LastName,
        Email = teamMember.Email,
        AssignedTasks = assignedTasksDTO,
        CreatedTasks = createdTasksDTO
    };

    return memberTasksDTO;
}

    public async Task<int> CreateTaskAsync(TaskCreationRequest taskRequest)
    {
        var task = new Task();
        task.Name = taskRequest.Name;
        task.Description = taskRequest.Description;
        task.Deadline = taskRequest.Deadline;
        task.IdProject = taskRequest.IdProject;
        task.IdTaskType = taskRequest.IdTaskType;
        task.IdAssignedTo = taskRequest.IdAssignedTo;
        task.IdCreator = taskRequest.IdCreator;
        
        Task created = await _taskRepository.CreateTask(task);
        return created.IdTask;
    }
}