namespace Domain.Models;
public class TaskAssignment
{
 public int Id { get; set; }
 public int TaskId { get; set; }
 public WorkTask Task { get; set; } = null!;
 public int UserId { get; set; }
 public User User { get; set; } = null!;
 public DateTime AssignedDate { get; set; }
}