using Application.Interfaces;
using Application.Interfaces.Infrastructure;
using Auth.Application.DTOs;
using Core.Entities;

namespace Application.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepo _articleRepository;
    private readonly IUserRepo _userRepo;

    public ArticleService(IArticleRepo articleRepository, IUserRepo userRepo)
    {
        _articleRepository = articleRepository;
        _userRepo = userRepo;
    }

    public async Task<IEnumerable<ArticleDTO>> GetAllArticlesAsync()
    {
        var articles = await _articleRepository.GetAllAsync();
        return articles.Select(a => new ArticleDTO
        {
            ArticleId = a.ArticleId,
            Title = a.Title,
            Content = a.Content,
            AuthorId = a.AuthorId,
            CreatedAt = a.CreatedAt
        });
    }

    public async Task<ArticleDTO> GetArticleByIdAsync(int id)
    {
        var article = await _articleRepository.GetByIdAsync(id);
        if (article == null) return null;
        return new ArticleDTO
        {
            ArticleId = article.ArticleId,
            Title = article.Title,
            Content = article.Content,
            AuthorId = article.AuthorId,
            CreatedAt = article.CreatedAt
        };
    }

    public async Task<ArticleDTO> AddArticleAsync(ArticleDTO articleDto)
    {
        // Check if the author exists
        var userExists = await _userRepo.GetByIdAsync(articleDto.AuthorId) != null;
        if (!userExists)
        {
            throw new KeyNotFoundException("Author not found. An article must have a valid author.");
        }

        // Create and save the new article
        var article = new Article
        {
            Title = articleDto.Title,
            Content = articleDto.Content,
            AuthorId = articleDto.AuthorId,
            CreatedAt = DateTime.UtcNow
        };
        await _articleRepository.AddAsync(article);
        articleDto.ArticleId = article.ArticleId;  // Reflect the ID back to the DTO after saving

        return articleDto;
    }


    public async Task UpdateArticleAsync(ArticleDTO articleDto)
    {
        var article = await _articleRepository.GetByIdAsync(articleDto.ArticleId);
        if (article != null)
        {
            article.Title = articleDto.Title;
            article.Content = articleDto.Content;
            await _articleRepository.UpdateAsync(article);
        }
    }

    public async Task DeleteArticleAsync(int id)
    {
        await _articleRepository.DeleteAsync(id);
    }
}