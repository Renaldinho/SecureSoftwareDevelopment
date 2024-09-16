using Application.Interfaces.Infrastructure;
using Core.Entities;

namespace Infrastructure;

public class CommentRepo : ICommentRepo
{
    private static List<Comment> _comments = new List<Comment>();
    private static int _nextId = 1;

    public async Task<IEnumerable<Comment>> GetAllAsync()
    {
        return await Task.FromResult(_comments);
    }

    public async Task<Comment> GetByIdAsync(int id)
    {
        var comment = _comments.FirstOrDefault(c => c.CommentId == id);
        return await Task.FromResult(comment);
    }

    public async Task AddAsync(Comment comment)
    {
        comment.CommentId = _nextId++;
        _comments.Add(comment);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(Comment comment)
    {
        var existingComment = _comments.FirstOrDefault(c => c.CommentId == comment.CommentId);
        if (existingComment != null)
        {
            existingComment.Content = comment.Content;
            existingComment.ArticleId = comment.ArticleId;
            existingComment.UserId = comment.UserId;
            // Assume created_at remains unchanged
        }
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var comment = _comments.FirstOrDefault(c => c.CommentId == id);
        if (comment != null)
        {
            _comments.Remove(comment);
        }
        await Task.CompletedTask;
    }
}