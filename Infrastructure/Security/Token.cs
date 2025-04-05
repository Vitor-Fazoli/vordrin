using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infrastructure.Dtos;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Security;

public class Token(IOptions<TokenSettings> tokenSettings) : ITokenService
{
    private readonly TokenSettings _tokenSettings = tokenSettings.Value;

    public TokenInfo GerarToken(UserDto usuario)
    {
        var claims = new List<Claim>
            {
                new(ClaimTypes.Name, usuario.Username),
                new(ClaimTypes.Email, usuario.Email),
                new (ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            };

        SymmetricSecurityKey key = new(
            Encoding.UTF8.GetBytes(_tokenSettings.Secret));

        SigningCredentials credential = new(
            key, SecurityAlgorithms.HmacSha256);

        DateTime expiracao = DateTime.UtcNow.AddHours(_tokenSettings.HoursUntilExpired);

        JwtSecurityToken token = new(
            issuer: _tokenSettings.Issuer,
            audience: _tokenSettings.Audience,
            claims: claims,
            expires: expiracao,
            signingCredentials: credential);

        return new TokenInfo
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiredAt = expiracao
        };
    }
}

public class TokenInfo
{
    public string Token { get; set; }
    public DateTime ExpiredAt { get; set; }
}

public interface ITokenService
{
    TokenInfo GerarToken(UserDto usuario);
}