using Application.Interfaces.Infrastructure;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class UserRepo : IUserRepo
{
    private readonly DatabaseContext _context;

    public UserRepo(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Set<User>().ToListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.Set<User>().FindAsync(id);
    }

    public async Task AddAsync(User entity)
    {
        _context.Set<User>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User entity)
    {
        var user = await _context.Set<User>().FindAsync(entity.UserId);
        if (user != null)
        {
            _context.Entry(user).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(User entity)
    {
        var user = await _context.Set<User>().FindAsync(entity.UserId);
        if (user != null)
        {
            _context.Set<User>().Remove(user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<User> GetByUsername(string username)
    {
        return await _context.Set<User>().FirstOrDefaultAsync(u => u.Username == username);
    }
}