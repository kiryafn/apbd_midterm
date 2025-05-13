namespace apbd_midterm.DTO;

public class MemberTasksDTO
{
    public int IdTeamMember { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public List<TaskDTO> AssignedTasks { get; set; }
    public List<TaskDTO> CreatedTasks { get; set; }
}