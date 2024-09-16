using Auth.Application.DTOs;
using Core.Entities;

namespace Application.Interfaces.Infrastructure;

public interface IUserRepo
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByIdAsync(int id);
    Task AddAsync(User entity);
    Task UpdateAsync(User entity);
    Task DeleteAsync(User entity);
    Task<User> GetByUsername(string username);
}