using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories;
public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;
    public async Task<List<User>> GetAll() =>
        await _context.Users.AsNoTracking().ToListAsync();
    public async Task<User?> GetById(int id) =>
        await _context.Users.FindAsync(id);
    public async Task<bool> Add(User user)
    {
        await _context.Users.AddAsync(user);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> Update(User user)
    {
        _context.Users.Update(user);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> Delete(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;
        _context.Users.Remove(user);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<List<User>> GetUsersWithMostTasks() =>
        await _context.Users.AsNoTracking()
            .Include(x => x.Tasks)
            .OrderByDescending(x => x.Tasks.Count)
            .ToListAsync();
}
