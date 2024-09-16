using Application.Interfaces;
using Auth.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{

    private readonly IUserService _userService;
    
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    // POST: api/Users
    [HttpPost("register")]
    public async Task<ActionResult<string>> PostUser(AuthenticationModel credentials)
    {
        string token = await _userService.Register(credentials);
        return Ok(token);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(AuthenticationModel credentials)
    {
        string token = await _userService.Login(credentials);
        return Ok(token);
    }
}