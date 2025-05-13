using System.ComponentModel.DataAnnotations;

namespace apbd_midterm.DTO;

public class TaskDTO
{
    public int IdTask { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public DateTime Deadline { get; set; }
    
    public string ProjectName { get; set; }
    
    public string TaskTypeName { get; set; }
}