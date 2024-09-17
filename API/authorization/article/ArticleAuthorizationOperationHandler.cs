using System.Security.Claims;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace API.authorization.article;

public class ArticleAuthorizationHandler : AuthorizationHandler<ArticleOperationRequirement, int>
{
    private readonly IArticleService _articleService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ArticleAuthorizationHandler(IArticleService articleService, IHttpContextAccessor httpContextAccessor)
    {
        _articleService = articleService;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ArticleOperationRequirement requirement, int articleId)
    {
        context.Succeed(requirement);
        return Task.CompletedTask;
        // var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        // var article = await _articleService.GetArticleByIdAsync(articleId);
        //
        // Console.WriteLine("Article id" + article.ArticleId.ToString());
        //
        // if (article == null)
        // {
        //     context.Fail();
        //     return;
        // }
        //
        // if (context.User.IsInRole("Editor") || (article.AuthorId.ToString() == userId))
        // {
        //     context.Succeed(requirement);
        // }
        // else
        // {
        //     context.Fail();
        // }
    }
}