using Core.Entities;

namespace Application.Interfaces.Infrastructure;

public interface ICommentRepo
{
    Task<IEnumerable<Comment>> GetAllAsync();
    Task<Comment> GetByIdAsync(int id);
    Task AddAsync(Comment comment);
    Task UpdateAsync(Comment comment);
    Task DeleteAsync(int id);
}