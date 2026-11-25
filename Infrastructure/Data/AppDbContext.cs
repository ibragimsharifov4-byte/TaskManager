
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<WorkTask> Tasks => Set<WorkTask>();
    public DbSet<User> Users => Set<User>();
    public DbSet<TaskAssignment> TaskAssignments => Set<TaskAssignment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Project>().ToTable("projects");
        modelBuilder.Entity<WorkTask>().ToTable("tasks");
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<TaskAssignment>().ToTable("task_assignments");
    }
}
