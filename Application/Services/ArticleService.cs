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

    public async Task<IEnumerable<Article>> GetAllArticlesAsync()
    {
        return await _articleRepository.GetAllAsync();
    }

    public async Task<Article> GetArticleByIdAsync(int id)
    {
        var article = await _articleRepository.GetByIdAsync(id);
        if (article == null)
        {
        }
        return article;
    }

    public Task<ArticleDTO> AddArticleAsync(ArticleDTO article)
    {
        throw new NotImplementedException();
    }

    public async Task AddArticleAsync(Article article)
    {
        await _articleRepository.AddAsync(article);
    }

    public async Task UpdateArticleAsync(Article article)
    {
        await _articleRepository.UpdateAsync(article);
    }

    public async Task DeleteArticleAsync(int id)
    {
        var article = await _articleRepository.GetByIdAsync(id);
        if (article != null)
        {
            await _articleRepository.DeleteAsync(id);
        }
        // Optionally handle the case where the article does not exist or already deleted
    }
}