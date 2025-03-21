using System.Threading.Tasks;
using aegis_server.Data;
using aegis_server.Models;
using aegis_server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aegis_server.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(AuthService authService, PlayerService playerService, AegisDbContext dbContext) : ControllerBase
{
    private readonly AuthService _authService = authService;
    private readonly AegisDbContext _context = dbContext;
    private readonly PlayerService _playerService = playerService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRequest request)
    {
        try
        {
            var user = _authService.Register(request.Username, request.Password);

            await _playerService.CreatePlayer(user.Id, request.Username);

            return Ok(new { user.Id, user.Username });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserRequest request)
    {
        var token = _authService.Login(request.Username, request.Password);
        if (token == null) return Unauthorized("Usuário ou senha inválidos.");
        return Ok(new { Token = token });
    }

    [HttpPost("refresh")]
    public IActionResult RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var storedRefreshToken = _context.RefreshTokens
            .FirstOrDefault(t => t.Token == request.RefreshToken && !t.IsRevoked);

        if (storedRefreshToken == null || storedRefreshToken.ExpiryDate < DateTime.UtcNow)
            return Unauthorized("Refresh token inválido ou expirado.");

        var user = _context.Users.FirstOrDefault(u => u.Id == storedRefreshToken.UserId);
        if (user == null)
            return Unauthorized("Usuário não encontrado.");

        var newAccessToken = _authService.GenerateJwtToken(user);  // Gerar novo access token
        var newRefreshToken = _authService.GenerateRefreshToken();  // Gerar novo refresh token

        // Revogar o refresh token antigo e salvar o novo
        storedRefreshToken.IsRevoked = true;
        _context.RefreshTokens.Add(new RefreshToken
        {
            UserId = user.Id,
            Token = newRefreshToken,
            ExpiryDate = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        });
        _context.SaveChanges();

        return Ok(new { AccessToken = newAccessToken, RefreshToken = newRefreshToken });
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetUser()
    {
        // Recuperando o token da requisição
        var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");

        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized("Token is missing.");
        }

        // Verificar se o token é válido e obter as informações do usuário
        var user = await _authService.GetUserFromToken(token); // Função que valida e decodifica o token

        if (user == null)
        {
            return Unauthorized("Invalid or expired token.");
        }

        return Ok(user); // Retorne os dados do usuário, como ID, nome, etc.
    }

}
public record RefreshTokenRequest(string RefreshToken);

public record UserRequest(string Username, string Password);