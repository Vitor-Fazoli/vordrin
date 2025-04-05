using Application.Dtos;
using Application.Responses;
using Domain.Entities;
using Infrastructure.Dtos;
using Infrastructure.Repositories;
using Infrastructure.Security;

namespace Application.Services;

public class AuthService(UserRepository usuarioRepository, Token tokenService)
{
    readonly UserRepository _usuarioRepository = usuarioRepository;
    readonly Token _tokenService = tokenService;

    public async Task<LoginResponse?> AutenticarAsync(LoginRequest request)
    {
        UserDto usuario = await _usuarioRepository.GetByCredentialAsync(request.Credential, request.Password);

        if (usuario == null)
            return null;

        var token = _tokenService.GerarToken(usuario);

        return new LoginResponse
        {
            Token = token.Token,
            ExpiredAt = token.ExpiredAt,
            Username = usuario.Username,
        };
    }
}

