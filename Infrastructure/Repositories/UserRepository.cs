using System.Data.Common;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Config;
using Infrastructure.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(VordrinDbContext db) : IUserRepository<UserDto>
{
    public async Task AddAsync(UserDto entity)
    {
        try
        {
            db.Users.Add(entity);
            await db.SaveChangesAsync();
        }
        catch (DbException)
        {
            throw;
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        try
        {
            UserDto user = db.Users.Where(u => u.Id == id).Single();

            db.Users.Remove(user);
            await db.SaveChangesAsync();
        }
        catch (DbException)
        {
            throw;
        }
    }

    public async Task<UserDto> GetByIdAsync(Guid id)
    {
        return await db.Users.Where(u => u.Id == id).SingleAsync();
    }

    public Task UpdateAsync(UserDto entity)
    {
        throw new NotImplementedException();
    }
}