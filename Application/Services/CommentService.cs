using Application.Interfaces;
using Application.Interfaces.Infrastructure;
using Core.Entities;

namespace Application.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepo _commentRepository;

    public CommentService(ICommentRepo commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
    {
        return await _commentRepository.GetAllAsync();
    }

    public async Task<Comment> GetCommentByIdAsync(int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null)
        {
            // Implement any business logic for handling not found
        }
        return comment;
    }

    public async Task AddCommentAsync(Comment comment)
    {
        // Perform any necessary business logic before adding a comment
        await _commentRepository.AddAsync(comment);
    }

    public async Task UpdateCommentAsync(Comment comment)
    {
        // Perform any necessary business logic before updating a comment
        await _commentRepository.UpdateAsync(comment);
    }

    public async Task DeleteCommentAsync(int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment != null)
        {
            await _commentRepository.DeleteAsync(id);
        }
        // Optionally handle the case where the comment does not exist
    }
}