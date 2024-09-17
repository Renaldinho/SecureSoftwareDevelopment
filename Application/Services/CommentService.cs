using Application.Interfaces;
using Application.Interfaces.Infrastructure;
using Auth.Application.DTOs;
using Core.Entities;

namespace Application.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepo _commentRepository;
    private readonly IArticleRepo _articleRepository;
    private readonly IUserRepo _userRepository;

    public CommentService(ICommentRepo commentRepository, IArticleRepo articleRepository, IUserRepo userRepository)
    {
        _commentRepository = commentRepository;
        _articleRepository = articleRepository;
        _userRepository = userRepository;
    }

    public async Task<CommentDTO> AddCommentAsync(CommentDTO commentDto)
    {
        // Validate that the user exists
        var userExists = await _userRepository.GetByIdAsync(commentDto.UserId) != null;
        if (!userExists)
        {
            throw new KeyNotFoundException("User not found.");
        }

        // Validate that the article exists
        var articleExists = await _articleRepository.GetByIdAsync(commentDto.ArticleId) != null;
        if (!articleExists)
        {
            throw new KeyNotFoundException("Article not found.");
        }

        // Create and save the new comment
        var comment = new Comment
        {
            Content = commentDto.Content,
            ArticleId = commentDto.ArticleId,
            UserId = commentDto.UserId,
            CreatedAt = DateTime.UtcNow
        };
        await _commentRepository.AddAsync(comment);
        commentDto.CommentId = comment.CommentId;

        return commentDto;
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