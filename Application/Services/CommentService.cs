using Application.Interfaces;
using Application.Interfaces.Infrastructure;
using Auth.Application.DTOs;
using Core.Entities;

namespace Application.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepo _commentRepository;

    public CommentService(ICommentRepo commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<IEnumerable<CommentDTO>> GetAllCommentsAsync()
    {
        var comments = await _commentRepository.GetAllAsync();
        return comments.Select(c => new CommentDTO
        {
            CommentId = c.CommentId,
            Content = c.Content,
            ArticleId = c.ArticleId,
            UserId = c.UserId,
            CreatedAt = c.CreatedAt
        });
    }

    public async Task<CommentDTO> GetCommentByIdAsync(int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null) return null;
        return new CommentDTO
        {
            CommentId = comment.CommentId,
            Content = comment.Content,
            ArticleId = comment.ArticleId,
            UserId = comment.UserId,
            CreatedAt = comment.CreatedAt
        };
    }

    public async Task<CommentDTO> AddCommentAsync(CommentDTO commentDto)
    {
        var comment = new Comment
        {
            Content = commentDto.Content,
            ArticleId = commentDto.ArticleId,
            UserId = commentDto.UserId
        };
        await _commentRepository.AddAsync(comment);
        commentDto.CommentId = comment.CommentId;  // Reflect the generated ID back to the DTO
        return commentDto;
    }

    public async Task UpdateCommentAsync(CommentDTO commentDto)
    {
        var comment = await _commentRepository.GetByIdAsync(commentDto.CommentId);
        if (comment != null)
        {
            comment.Content = commentDto.Content;
            await _commentRepository.UpdateAsync(comment);
        }
    }

    public async Task DeleteCommentAsync(int id)
    {
        await _commentRepository.DeleteAsync(id);
    }
}