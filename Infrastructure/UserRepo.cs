using Application.Interfaces.Infrastructure;
using Core.Entities;

namespace Infrastructure;

public class UserRepo: IUserRepo
{
    private static List<User> _users = new List<User>();
    private static int _nextId = 1;  // Simulates auto-increment ID in a database

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        // Return a copy of the list to simulate asynchronous database access
        return await Task.FromResult(_users.ToList());
    }

    public async Task<User> GetByIdAsync(int id)
    {
        // Simulate asynchronous database access
        var user = _users.FirstOrDefault(u => u.UserId == id);
        return await Task.FromResult(user);
    }

    public async Task AddAsync(User entity)
    {
        // Assume the User object is fully initialized when passed to this method
        entity.UserId = _nextId++;
        _users.Add(entity);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(User entity)
    {
        var user = _users.FirstOrDefault(u => u.UserId == entity.UserId);
        if (user != null)
        {
            // Update the properties
            user.Username = entity.Username;
            user.PasswordHash = entity.PasswordHash;
            // Normally you would handle more properties and handle concurrency
        }
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(User entity)
    {
        var user = _users.FirstOrDefault(u => u.UserId == entity.UserId);
        if (user != null)
        {
            _users.Remove(user);
        }
        await Task.CompletedTask;
    }
    
    public async Task<User> GetByUsername(string username)
    {
        // Simulate asynchronous database access
        var user = _users.FirstOrDefault(u => u.Username == username);
        return await Task.FromResult(user);
    }
}