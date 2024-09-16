using Application.Interfaces.Infrastructure;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ArticleRepo : IArticleRepo
{
    private readonly DatabaseContext _context;

    public ArticleRepo(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Article>> GetAllAsync()
    {
        return await _context.Articles.ToListAsync();
    }

    public async Task<Article> GetByIdAsync(int id)
    {
        return await _context.Articles.FindAsync(id);
    }

    public async Task AddAsync(Article article)
    {
        _context.Articles.Add(article);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Article article)
    {
        _context.Articles.Update(article);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var article = await GetByIdAsync(id);
        if (article != null)
        {
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }
    }
}