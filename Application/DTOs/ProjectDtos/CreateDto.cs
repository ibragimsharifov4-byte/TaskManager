using System.ComponentModel.DataAnnotations;
namespace Application.DTOs.ProjectDtos;
public class CreateDto
{
 [Required, MaxLength(120)] public string Name { get; set; } = null!;
[MaxLength(1000)] public string Description { get; set; } = null!;
public DateTime StartDate { get; set; }
public DateTime? EndDate { get; set; }
}