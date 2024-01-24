using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace ExamServer.Services;

public interface IJwtGenerator
{
    string GenerateToken(User user);
}

public class JwtGenerator : IJwtGenerator
{
    private readonly SymmetricSecurityKey _key;
    private readonly string Audience;
    private readonly string Issuer;

    public JwtGenerator(IConfiguration config)
    {
        Issuer = config["JwtSettings:Issuer"];
        Audience = config["JwtSettings:Audience"];
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]));
    }
    
    public string GenerateToken(User user)
    {
        var claims = new List<Claim> {new(ClaimTypes.Name, user.UserName!) };

        var jwt = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(_key, SecurityAlgorithms.HmacSha256));
            
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}