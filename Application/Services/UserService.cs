using Application.Interfaces;
using Application.Requests;
using Application.Responses;
using Infrastructure.Config;
using Infrastructure.Repositories;
using Infrastructure.Security;

namespace Application.Services;

public class UserService(VordrinDbContext context) : IUserService
{
    private readonly VordrinDbContext _context = context;

    public async Task Create(UserRequest user)
    {
        try
        {
            user.Password = Crypt.HashPassword(user.Password);
            UserRepository repository = new(_context);
            await repository.AddAsync(user.Parse());
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<UserResponse> GetUserById(Guid id)
    {
        try
        {
            UserRepository repository = new(_context);
            return new UserResponse(await repository.GetByIdAsync(id));
        }
        catch (Exception)
        {
            throw;
        }
    }
}