using System.ComponentModel.DataAnnotations;
namespace Application.DTOs.TaskAssignmentDtos;
public class UpdateDto
{
 [Range(1, int.MaxValue)] public int TaskId { get; set; }
[Range(1, int.MaxValue)] public int UserId { get; set; }
public DateTime AssignedDate { get; set; }
}