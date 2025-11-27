using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BackendPractice.Beginner.TodoListAPI.Data;
using BackendPractice.Beginner.TodoListAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace BackendPractice.Beginner.TodoListAPI.Services;

public class JwtTokenProvider
{
    private readonly IConfiguration _configuration;

    public JwtTokenProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenResponse? GenerateToken(TokenRequest tokenRequest)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");

        var issuer = jwtSettings["Issuer"]!;
        var audience = jwtSettings["Audience"]!;
        var key = jwtSettings["SecretKey"]!;
        var expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["TokenExpirationInMinutes"]!));

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, tokenRequest.Id.ToString()),
            new Claim(ClaimTypes.Email, tokenRequest.Email),
            new Claim(ClaimTypes.Name, tokenRequest.Name)
        };

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(descriptor);

        return new TokenResponse
        {
            AceessToken = tokenHandler.WriteToken(securityToken),
            RefreshToken = "7a6f23b4e1d04c9a8f5b6d7c8a9e01f1",
            ExpiresIn = expires
        };
    }
}
