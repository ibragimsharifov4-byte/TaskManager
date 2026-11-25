namespace Domain.Interfaces;
using Domain.Models;
public interface IUserRepository
{
    Task<List<User>> GetAll();
    Task<User?> GetById(int id);
    Task<bool> Add(User user);
    Task<bool> Update(User user);
    Task<bool> Delete(int id);
    Task<List<User>> GetUsersWithMostTasks();
}
