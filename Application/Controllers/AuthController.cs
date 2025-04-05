using Application.Interfaces;
using Application.Requests;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService autenticacaoService) : ControllerBase
{
    private readonly IAuthService _authService = autenticacaoService;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!request.Validate())
            return BadRequest("Dados de login inválidos");

        LoginResponse response = await _authService.AuthAsync(request);

        if (response is null)
            return Unauthorized("Email ou senha inválidos");

        return Ok(response);
    }
}