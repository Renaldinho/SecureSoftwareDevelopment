using Auth.Application.DTOs;
using Core.Entities;

namespace Application.Interfaces;

public interface IArticleService
{
    Task<IEnumerable<Article>> GetAllArticlesAsync();
    Task<Article> GetArticleByIdAsync(int id);
    Task<ArticleDTO> AddArticleAsync(ArticleDTO article);
    Task UpdateArticleAsync(Article article);
    Task DeleteArticleAsync(int id);
}