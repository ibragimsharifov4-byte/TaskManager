namespace Domain.Interfaces;
using Domain.Models;
public interface IProjectRepository
{
    Task<List<Project>> GetAll();
    Task<Project?> GetById(int id);
    Task<bool> Add(Project project);
    Task<bool> Update(Project project);
    Task<bool> Delete(int id);
    Task<List<Project>> GetProjectsWithMostTasks();
}
