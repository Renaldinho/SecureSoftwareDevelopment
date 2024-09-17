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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArticleDTO>>> GetArticles()
    {
        var articles = await _articleService.GetAllArticlesAsync();
        return Ok(articles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ArticleDTO>> GetArticle(int id)
    {
        var article = await _articleService.GetArticleByIdAsync(id);
        if (article == null)
        {
            return NotFound();
        }
        return Ok(article);
    }

    [HttpPost]
    public async Task<ActionResult<ArticleDTO>> PostArticle([FromBody] ArticleDTO articleDto)
    {
        var newArticle = await _articleService.AddArticleAsync(articleDto);
        return CreatedAtAction(nameof(GetArticle), new { id = newArticle.ArticleId }, newArticle);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateArticle(int id, [FromBody] ArticleDTO articleDto)
    {
        if (id != articleDto.ArticleId)
        {
            return BadRequest();
        }
        await _articleService.UpdateArticleAsync(articleDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArticle(int id)
    {
        await _articleService.DeleteArticleAsync(id);
        return NoContent();
    }
}