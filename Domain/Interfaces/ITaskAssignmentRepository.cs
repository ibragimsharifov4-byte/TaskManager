namespace Domain.Interfaces;
using Domain.Models;
public interface ITaskAssignmentRepository
{
    Task<List<TaskAssignment>> GetAll();
    Task<TaskAssignment?> GetById(int id);
    Task<bool> Add(TaskAssignment assignment);
    Task<bool> Update(TaskAssignment assignment);
    Task<bool> Delete(int id);
    Task<List<TaskAssignment>> GetAssignmentsByUser(int userId);
    Task<List<TaskAssignment>> GetAssignmentsByTask(int taskId);
}
