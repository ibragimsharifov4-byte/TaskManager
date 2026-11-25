namespace Domain.Models;
public class User
{
 public int Id { get; set; }
 public string Name { get; set; } = null!;
 public string Email { get; set; } = null!;
 public DateTime RegistrationDate { get; set; }
 public List<WorkTask> Tasks { get; set; } = new();
 public List<TaskAssignment> Assignments { get; set; } = new();
}