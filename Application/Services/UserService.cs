using System.Data.Common;
using System.Threading.Tasks;
using Infrastructure.Config;
using Infrastructure.Dtos;
using Infrastructure.Repositories;
using Infrastructure.Security;

namespace Application.Services;

public class UserService(VordrinDbContext context)
{
    private readonly VordrinDbContext _context = context;

    public async Task Create(UserDto user)
    {
        try
        {
            user.Password = Crypt.HashPassword(user.Password);
            UserRepository repository = new(_context);
            await repository.AddAsync(user);
        }
        catch (Exception)
        {
            throw;
        }
    }
}