namespace Domain.Models;
public class WorkTask
{
 public int Id { get; set; }
 public string Title { get; set; } = null!;
 public string Description { get; set; } = null!;
 public DateTime DueDate { get; set; }
 public int ProjectId { get; set; }
 public Project Project { get; set; } = null!;
 public int UserId { get; set; }
 public User User { get; set; } = null!;
 public List<TaskAssignment> Assignments { get; set; } = new();
}