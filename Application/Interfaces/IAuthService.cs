using Application.Requests;
using Application.Responses;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> AuthAsync(LoginRequest request);
}