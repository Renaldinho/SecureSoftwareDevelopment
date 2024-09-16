using Core.Entities;

namespace Application.Interfaces;

public interface ICommentService
{
    Task<IEnumerable<Comment>> GetAllCommentsAsync();
    Task<Comment> GetCommentByIdAsync(int id);
    Task AddCommentAsync(Comment comment);
    Task UpdateCommentAsync(Comment comment);
    Task DeleteCommentAsync(int id);
}