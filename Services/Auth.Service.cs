using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TodoList.Data.Models;
using TodoList.DTOs;
using TodoList.Interfaces;

namespace TodoList.Services;

public class AuthService : IAuth
{
    private readonly UserManager<User> userManager;
    private readonly IConfiguration config;

    public AuthService(UserManager<User> userManager, IConfiguration configuration)
    {
        this.userManager = userManager;
        config = configuration;
    }

    public async Task<bool> Register(RegisterDto dto)
    {
        if (dto != null)
        {
            User user = new User()
            {
                UserName = dto.Username,
                Email = dto.Eamil
            };
            var result = await userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
                return true;
        }
        return false;
    }
    public async Task<string?> Login(LoginDto dto)
    {
        var user = await userManager.FindByNameAsync(dto.Username);

        if (user == null)
            return null;

        var isPasswordValid = await userManager.CheckPasswordAsync(user, dto.Password);

        if (!isPasswordValid)
            return null;


        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id!),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email?? "")
        };

        // Key
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecretKey"]!));
        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: config["JWT:Issuer"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credential
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
