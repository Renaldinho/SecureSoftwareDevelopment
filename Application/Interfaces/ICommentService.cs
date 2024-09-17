using Auth.Application.DTOs;

namespace Application.Interfaces;

public interface ICommentService
{
    Task<IEnumerable<CommentDTO>> GetAllCommentsAsync();
    Task<CommentDTO> GetCommentByIdAsync(int id);
    Task<CommentDTO> AddCommentAsync(CommentDTO commentDto);
    Task UpdateCommentAsync(CommentDTO commentDto);
    Task DeleteCommentAsync(int id);
}