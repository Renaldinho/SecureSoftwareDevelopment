using Auth.Application.DTOs;
using Core.Entities;

namespace Application.Interfaces;

public interface IArticleService
{
    Task<IEnumerable<ArticleDTO>> GetAllArticlesAsync();
    Task<ArticleDTO> GetArticleByIdAsync(int id);
    Task<ArticleDTO> AddArticleAsync(ArticleDTO articleDto);
    Task UpdateArticleAsync(ArticleDTO articleDto);
    Task DeleteArticleAsync(int id);
}