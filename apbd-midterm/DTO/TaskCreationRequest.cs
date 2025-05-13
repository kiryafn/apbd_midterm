using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace apbd_midterm.DTO;

public class TaskCreationRequest
{
    [NotNull]
    [Required]
    public string Name { get; set; }
    [NotNull]
    [Required]
    public string Description { get; set; }
    [NotNull]
    [Required]
    public DateTime Deadline { get; set; }
    [NotNull]
    [Required]
    public int IdProject { get; set; }
    [NotNull]
    [Required]
    public int IdTaskType { get; set; }
    [NotNull]
    [Required]
    public int IdAssignedTo { get; set; }
    [NotNull]
    [Required]
    public int IdCreator{ get; set; }
}