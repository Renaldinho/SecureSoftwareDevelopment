using Application.Interfaces;
using Application.Interfaces.Infrastructure;
using Auth.Application.DTOs;
using Core.Entities;

namespace Application.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepo _articleRepository;

    public ArticleService(IArticleRepo articleRepository)
    {
        _articleRepository = articleRepository;
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
        var article = new Article
        {
            Title = articleDto.Title,
            Content = articleDto.Content,
            AuthorId = articleDto.AuthorId
        };
        await _articleRepository.AddAsync(article);
        articleDto.ArticleId = article.ArticleId;
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