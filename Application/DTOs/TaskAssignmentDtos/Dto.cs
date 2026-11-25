namespace Application.DTOs.TaskAssignmentDtos;
public class Dto
{
 public int Id { get; set; }
public int TaskId { get; set; }
public int UserId { get; set; }
public DateTime AssignedDate { get; set; }
}