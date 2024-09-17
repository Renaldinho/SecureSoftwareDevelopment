using API.authorization.article;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin() // Allow any origin
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthorizationHandler, ArticleAuthorizationHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanEditArticle", policy =>
        policy.AddRequirements(new ArticleOperationRequirement("Edit")));
    options.AddPolicy("CanDeleteArticle", policy =>
        policy.AddRequirements(new ArticleOperationRequirement("Delete")));

    options.AddPolicy("CanManageComments", policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole("Subscriber") && context.User.HasClaim("CommentId", context.Resource.ToString())));
});


builder.Services.AddDbContext<DatabaseContext>();
Application.DependencyResolver.DependencyResolverService.RegisterApplicationLayer(builder.Services, builder.Configuration);
Infrastructure.DependencyResolver.DependencyResolverService.RegisterInfrastructureLayer(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();  // Add this before UseAuthorization
app.UseAuthorization();

// app.UseHttpsRedirection();

app.MapControllers();

app.UseCors();

app.Run();
