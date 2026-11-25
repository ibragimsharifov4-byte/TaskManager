using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories;
public class WorkTaskRepository(AppDbContext context) : IWorkTaskRepository
{
    private readonly AppDbContext _context = context;
    public async Task<List<WorkTask>> GetAll() =>
        await _context.Tasks.AsNoTracking().ToListAsync();
    public async Task<WorkTask?> GetById(int id) =>
        await _context.Tasks.FindAsync(id);
    public async Task<bool> Add(WorkTask task)
    {
        await _context.Tasks.AddAsync(task);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> Update(WorkTask task)
    {
        _context.Tasks.Update(task);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> Delete(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return false;
        _context.Tasks.Remove(task);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<List<WorkTask>> GetTasksByProject(int projectId) =>
        await _context.Tasks.AsNoTracking().Where(x => x.ProjectId == projectId).ToListAsync();
    public async Task<List<WorkTask>> GetTasksByUser(int userId) =>
        await _context.Tasks.AsNoTracking().Where(x => x.UserId == userId).ToListAsync();
    public async Task<List<WorkTask>> GetTasksDueSoon(int days)
    {
        var now = DateTime.UtcNow;
        return await _context.Tasks.AsNoTracking()
            .Where(x => x.DueDate >= now && x.DueDate <= now.AddDays(days))
            .OrderBy(x => x.DueDate).ToListAsync();
    }
}
