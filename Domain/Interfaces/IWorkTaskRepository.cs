namespace Domain.Interfaces;
using Domain.Models;
public interface IWorkTaskRepository
{
    Task<List<WorkTask>> GetAll();
    Task<WorkTask?> GetById(int id);
    Task<bool> Add(WorkTask task);
    Task<bool> Update(WorkTask task);
    Task<bool> Delete(int id);
    Task<List<WorkTask>> GetTasksByProject(int projectId);
    Task<List<WorkTask>> GetTasksByUser(int userId);
    Task<List<WorkTask>> GetTasksDueSoon(int days);
}
