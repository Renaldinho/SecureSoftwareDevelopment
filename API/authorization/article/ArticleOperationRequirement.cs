using Microsoft.AspNetCore.Authorization;

namespace API.authorization.article;

public class ArticleOperationRequirement : IAuthorizationRequirement
{
    public string OperationName { get; }

    public ArticleOperationRequirement(string operationName)
    {
        OperationName = operationName;
    }
}