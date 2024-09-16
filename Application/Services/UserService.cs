using Application.Interfaces;
using Application.Interfaces.Infrastructure;
using Auth.Application.DTOs;
using Auth.Application.Interfaces;
using Core.Entities;

namespace Application.Services;

public class UserService: IUserService
{

    private readonly IEncryptionService _encryptionService;
    private readonly ITokenService _tokenService;
    private readonly IUserRepo _userRepo;

    public UserService(IEncryptionService encryptionService, ITokenService tokenService, IUserRepo userRepo)
    {
        _encryptionService = encryptionService;
        _tokenService = tokenService;
        _userRepo = userRepo;
    }
    
    public async Task<string> Register(AuthenticationModel credentials)
    {
        _encryptionService.CreatePasswordHash(credentials.Password, out byte[] passwordHash, out byte[] passwordSalt);
        
        User user = new User()
        {
            Username = credentials.Username,
            PasswordHash = passwordHash,
            Salt = passwordSalt,
        };

        await _userRepo.AddAsync(user);


        string bearerToken = _tokenService.GenerateToken(user);
        return bearerToken;
    }

    public async Task<string> Login(AuthenticationModel credentials)
    {
        User user = await _userRepo.GetByUsername(credentials.Username);
        
        if(_encryptionService.VerifyPasswordHash(credentials.Password, user.PasswordHash, user.Salt))
        {
            return _tokenService.GenerateToken(user);
        }
        throw new Exception("Credentials are wrong");
    }
}