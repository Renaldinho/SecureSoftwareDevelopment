using Application.Interfaces.Infrastructure;
using Core.Entities;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyResolver;

public static class DependencyResolverService
{
    public static void RegisterInfrastructureLayer(IServiceCollection services)
    {
        services.AddScoped<IUserRepo, UserRepo>();
        services.AddScoped<IArticleRepo, ArticleRepo>();
        services.AddScoped<ICommentRepo, CommentRepo>();
    }
}