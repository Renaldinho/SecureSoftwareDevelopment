using System.Security.Claims;
using Application.Interfaces;
using Auth.Application.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticlesController : ControllerBase
{
    private readonly IArticleService _articleService;
    private readonly IAuthorizationService _authorizationService;

    public ArticlesController(IArticleService articleService, IAuthorizationService authorizationService)
    {
        _articleService = articleService;
        _authorizationService = authorizationService;
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArticleDTO>>> GetArticles()
    {
        var articles = await _articleService.GetAllArticlesAsync();
        return Ok(articles);
    }

    [AllowAnonymous]
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
    [Authorize(Roles = "Editor,Writer")]
    public async Task<ActionResult<ArticleDTO>> PostArticle([FromBody] ArticleDTO articleDto)
    {
        var newArticle = await _articleService.AddArticleAsync(articleDto);
        return CreatedAtAction(nameof(GetArticle), new { id = newArticle.ArticleId }, newArticle);
    }
    
    [Authorize(Roles = "Editor,Writer")]
    [HttpPut("{id}")]
    public async Task<IActionResult> EditArticle(int id, [FromBody] ArticleDTO articleDto)
    {
        var article = await _articleService.GetArticleByIdAsync(id);
        if (article == null)
        {
            return NotFound();
        }

        // Get the current user's ID
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // Check if the current user is the author of the article or an editor
        if (article.AuthorId.ToString() == userId || User.IsInRole("Editor"))
        {
            // Assuming a method exists to update an article
            await _articleService.UpdateArticleAsync(articleDto);
            return Ok(articleDto);
        }
        else
        {
            return Forbid("You do not have permission to edit this article.");
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Editor,Writer")]
    public async Task<IActionResult> DeleteArticle(int id)
    {
        var article = await _articleService.GetArticleByIdAsync(id);
        if (article == null)
        {
            return NotFound();
        }

        // Get the current user's ID
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // Check if the current user is the author of the article or an editor
        if (article.AuthorId.ToString() == userId || User.IsInRole("Editor"))
        {
            await _articleService.DeleteArticleAsync(id);
            return NoContent();
        }
        else
        {
            return Forbid("You do not have permission to delete this article.");
        }
    }
}