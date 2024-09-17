using Application.Interfaces;
using Auth.Application.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticlesController : ControllerBase
{
    private readonly IArticleService _articleService;

    public ArticlesController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    // GET: api/Articles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
    {
        return Ok(await _articleService.GetAllArticlesAsync());
    }

    // GET: api/Articles/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Article>> GetArticle(int id)
    {
        var article = await _articleService.GetArticleByIdAsync(id);
        if (article == null)
        {
            return NotFound();
        }
        return Ok(article);
    }

    // POST: api/Articles
    [HttpPost]
    public async Task<ActionResult<Article>> PostArticle([FromBody] ArticleDTO article)
    {
        await _articleService.AddArticleAsync(article);
        return CreatedAtAction(nameof(GetArticle), new { id = article.ArticleId }, article);
    }

    // PUT: api/Articles/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutArticle(int id, [FromBody] Article article)
    {
        if (id != article.ArticleId)
        {
            return BadRequest();
        }

        await _articleService.UpdateArticleAsync(article);
        return NoContent();
    }

    // DELETE: api/Articles/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArticle(int id)
    {
        var article = await _articleService.GetArticleByIdAsync(id);
        if (article == null)
        {
            return NotFound();
        }

        await _articleService.DeleteArticleAsync(id);
        return NoContent();
    }
}