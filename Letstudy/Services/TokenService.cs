using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Letstudy.Models;
using Microsoft.IdentityModel.Tokens;

namespace Letstudy.Services;

public class TokenService
{
    private readonly string? _securiryKey;
    private readonly int _accessTokenExpirationInMinutes;
    private readonly string? _issuer;
    private readonly string? _audience;

    public TokenService(IConfiguration configuration)
    {
        var configuration1 = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _securiryKey = configuration1.GetValue<string>("SecurityKey");
        _issuer = configuration1.GetValue<string>("Issuer");
        _audience = configuration1.GetValue<string>("Audience");
        _accessTokenExpirationInMinutes = configuration.GetValue("AccessTokenExpirationInMinutes", 60);
    }

    public string GenerateToken(User user)
    {
        if (_securiryKey == null || _audience == null || _issuer == null)
        {
            throw new InvalidOperationException("Invalid secret data");
        }
        
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securiryKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_accessTokenExpirationInMinutes),
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}