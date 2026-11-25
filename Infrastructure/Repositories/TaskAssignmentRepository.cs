using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories;
public class TaskAssignmentRepository(AppDbContext context) : ITaskAssignmentRepository
{
    private readonly AppDbContext _context = context;
    public async Task<List<TaskAssignment>> GetAll() =>
        await _context.TaskAssignments.AsNoTracking().ToListAsync();
    public async Task<TaskAssignment?> GetById(int id) =>
        await _context.TaskAssignments.FindAsync(id);
    public async Task<bool> Add(TaskAssignment assignment)
    {
        await _context.TaskAssignments.AddAsync(assignment);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> Update(TaskAssignment assignment)
    {
        _context.TaskAssignments.Update(assignment);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> Delete(int id)
    {
        var assignment = await _context.TaskAssignments.FindAsync(id);
        if (assignment == null) return false;
        _context.TaskAssignments.Remove(assignment);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<List<TaskAssignment>> GetAssignmentsByUser(int userId) =>
        await _context.TaskAssignments.AsNoTracking().Where(x => x.UserId == userId).ToListAsync();
    public async Task<List<TaskAssignment>> GetAssignmentsByTask(int taskId) =>
        await _context.TaskAssignments.AsNoTracking().Where(x => x.TaskId == taskId).ToListAsync();
}
