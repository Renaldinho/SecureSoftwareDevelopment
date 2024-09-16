using Auth.Application.DTOs;

namespace Application.Interfaces;

public interface IUserService
{
    Task<string> Register(AuthenticationModel credentials);

    Task<string> Login(AuthenticationModel credentials);
}