using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using aegis_server.Data;
using dotenv.net;
using aegis_server.Models;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace aegis_server.Services;

public class AuthService(AegisDbContext context)
{
    private readonly AegisDbContext _context = context;
    private readonly string secret = DotEnv.Read()["SECRET"];
    private readonly string _refreshTokenKey = DotEnv.Read()["SECRET_REFRESH"];

    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[32]; // Define o tamanho do token (32 bytes = 256 bits)
        using (var rng = RandomNumberGenerator.Create()) // Usando o novo método para gerar números aleatórios criptograficamente seguros
        {
            rng.GetBytes(randomBytes); // Preenche o array com números aleatórios
        }
        return Convert.ToBase64String(randomBytes); // Gera um refresh token seguro e o converte para string base64
    }

    private void SaveRefreshToken(Guid userId, string refreshToken)
    {
        var token = new RefreshToken
        {
            UserId = userId.ToString(),
            Token = refreshToken,
            ExpiryDate = DateTime.UtcNow.AddDays(7), // O refresh token expira em 7 dias
            IsRevoked = false
        };
        _context.RefreshTokens.Add(token);
        _context.SaveChanges();
    }

    public User Register(string username, string password)
    {
        if (_context.Users.Any(u => u.Username == username))
            throw new Exception("User already exists!");

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        var user = new User
        {
            Username = username,
            PasswordHash = passwordHash
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return user;
    }

    public string? Login(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return null;

        return GenerateJwtToken(user);
    }

    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Username),
            ]),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string? ValidateJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var env = DotEnv.Read();

            var key = Encoding.ASCII.GetBytes(secret);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

            if (validatedToken is JwtSecurityToken jwtToken)
            {
                // Supondo que você tenha um campo 'userId' no token
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;

                return userId;
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }


    public async Task<User> GetUserFromToken(string token)
    {
        try
        {
            // Validar o JWT
            var userInfo = ValidateJwtToken(token);

            if (userInfo == null)
            {
                return null;
            }

            // Obter informações do usuário a partir do banco de dados com base no ID do usuário
            var user = await _context.Users
                .Where(u => u.Id == userInfo)
                .FirstOrDefaultAsync();

            return user;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
