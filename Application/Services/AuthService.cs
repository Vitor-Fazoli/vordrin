using Application.Interfaces;
using Application.Requests;
using Application.Responses;
using Domain.Interfaces;
using Infrastructure.Dtos;
using Infrastructure.Repositories;
using Infrastructure.Security;

namespace Application.Services;

public class AuthService(IUserRepository<UserDto> usuarioRepository, ITokenService tokenService) : IAuthService
{
    readonly IUserRepository<UserDto> _usuarioRepository = usuarioRepository;
    readonly ITokenService _tokenService = tokenService;

    public async Task<LoginResponse> AuthAsync(LoginRequest request)
    {
        UserDto usuario = await _usuarioRepository.GetByCredentialAsync(request.Credential, request.Password);

        if (usuario == null)
            return null!;

        var token = _tokenService.GerarToken(usuario);

        return new LoginResponse
        {
            Id = usuario.Id.ToString(),
            Token = token.Token,
            ExpiredAt = token.ExpiredAt,
            Username = usuario.Username,
        };
    }
}

