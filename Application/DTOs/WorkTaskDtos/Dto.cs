namespace Application.DTOs.WorkTaskDtos;
public class Dto
{
 public int Id { get; set; }
public string Title { get; set; } = null!;
public string Description { get; set; } = null!;
public DateTime DueDate { get; set; }
public int ProjectId { get; set; }
public int UserId { get; set; }
}