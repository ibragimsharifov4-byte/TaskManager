using System.ComponentModel.DataAnnotations;
namespace Application.DTOs.WorkTaskDtos;
public class CreateDto
{
 [Required, MaxLength(200)] public string Title { get; set; } = null!;
[MaxLength(1000)] public string Description { get; set; } = null!;
public DateTime DueDate { get; set; }
[Range(1, int.MaxValue)] public int ProjectId { get; set; }
[Range(1, int.MaxValue)] public int UserId { get; set; }
}