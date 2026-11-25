using Domain.Models;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Data;
public static class DbSeeder
{
 public static async Task SeedAsync(AppDbContext db)
 {
  await db.Database.MigrateAsync();
  if(await db.Projects.AnyAsync()) return;
  var project=new Project { Name="Website development", Description="Create company website", StartDate=DateTime.UtcNow.Date };
  var user1=new User { Name="Alex", Email="alex@example.com", RegistrationDate=DateTime.UtcNow };
  var user2=new User { Name="Maria", Email="maria@example.com", RegistrationDate=DateTime.UtcNow };
  db.Projects.Add(project); db.Users.AddRange(user1,user2);
  await db.SaveChangesAsync();
  var task1=new WorkTask { Title="Design homepage", Description="Prepare homepage layout", DueDate=DateTime.UtcNow.AddDays(3), ProjectId=project.Id, UserId=user1.Id };
  var task2=new WorkTask { Title="Create API", Description="Implement backend endpoints", DueDate=DateTime.UtcNow.AddDays(7), ProjectId=project.Id, UserId=user2.Id };
  db.Tasks.AddRange(task1,task2); await db.SaveChangesAsync();
  db.TaskAssignments.AddRange(new TaskAssignment { TaskId=task1.Id, UserId=user1.Id, AssignedDate=DateTime.UtcNow }, new TaskAssignment { TaskId=task2.Id, UserId=user2.Id, AssignedDate=DateTime.UtcNow });
  await db.SaveChangesAsync();
 }
}