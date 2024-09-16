using Core.Entities;

namespace Application.Interfaces.Infrastructure;

public interface IArticleRepo
{
    Task<IEnumerable<Article>> GetAllAsync();
    Task<Article> GetByIdAsync(int id);
    Task AddAsync(Article article);
    Task UpdateAsync(Article article);
    Task DeleteAsync(int id);
}