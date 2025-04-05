using Application.Requests;
using Application.Responses;

namespace Application.Interfaces;

public interface IUserService
{
    public Task Create(UserRequest user);
    public Task<UserResponse> GetUserById(Guid id);
}