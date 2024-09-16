using Core.Entities;

namespace Auth.Application.Interfaces;

public interface ITokenService
{
    public string GenerateToken(User credentials);
}