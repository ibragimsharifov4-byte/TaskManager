using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories;
public class ProjectRepository(AppDbContext context) : IProjectRepository
{
    private readonly AppDbContext _context = context;
    public async Task<List<Project>> GetAll() =>
        await _context.Projects.AsNoTracking().ToListAsync();
    public async Task<Project?> GetById(int id) =>
        await _context.Projects.FindAsync(id);
    public async Task<bool> Add(Project project)
    {
        await _context.Projects.AddAsync(project);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> Update(Project project)
    {
        _context.Projects.Update(project);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> Delete(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return false;
        _context.Projects.Remove(project);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<List<Project>> GetProjectsWithMostTasks() =>
        await _context.Projects.AsNoTracking()
            .Include(x => x.Tasks)
            .OrderByDescending(x => x.Tasks.Count)
            .ToListAsync();
}
