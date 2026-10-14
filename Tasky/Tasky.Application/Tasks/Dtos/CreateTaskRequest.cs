using System.ComponentModel.DataAnnotations;

namespace Tasky.Application.Tasks.Dtos;

public class CreateTaskRequest
{
    [Required]
    public required string Title { get; set; }
    public string? Description { get; set; }
    public Guid? ResponsibleId { get; set; }
    [Range(0, int.MaxValue)]
    public int? Estimate { get; set; }
    public DateTime? DueDate { get; set; }
}
