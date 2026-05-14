using FluentValidation;
using Letstudy.Core;
using Letstudy.Models;
using Letstudy.Requests;
using Letstudy.Responses;
using Letstudy.Services;
using Letstudy.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Letstudy.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    TokenService tokenService,
    UserService userService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
    {
        var duplicateUser = await userService.FindUserByEmailAsync(request.Email);
        if (duplicateUser != null) return Conflict("Некорректный адрес");

        var user = CreateUserByRole(request);
        
        await userService.CreateAsync(user);
        return Ok(new RegisterResponse(user.Id));
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
    {
        var user = await userService.FindUserByEmailAsync(request.Email);
        if (user == null || !PasswordHasher.Verify(request.Password, user.PasswordHash))
        {
            return Unauthorized("Неверный логин или пароль");
        }
        
        var token = tokenService.GenerateToken(user);
        return Ok(new LoginResponse(token));
    }

    private static User CreateUserByRole(RegisterRequest request)
    {
        return request.Role switch
        {
            UserRole.Student => new Student
            {
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
                PasswordHash = PasswordHasher.Hash(request.Password),
                Role = UserRole.Student
            },
            UserRole.Tutor => new Tutor
            {
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
                PasswordHash = PasswordHasher.Hash(request.Password),
                Role = UserRole.Tutor
            },
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}