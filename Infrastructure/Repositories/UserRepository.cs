using System.Data.Common;
using Domain.Interfaces;
using Infrastructure.Config;
using Infrastructure.Dtos;
using Infrastructure.Security;
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

    /// <summary>
    /// Get a user by email or username.
    /// This method is used for authentication purposes.
    /// </summary>
    /// <param name="credential"></param>
    /// <returns></returns>
    public async Task<UserDto> GetByCredentialAsync(string credential, string password)
    {
        if (string.IsNullOrEmpty(credential) || string.IsNullOrEmpty(password))
            return null!;

        UserDto? user = await db.Users
            .Where(u => u.Username == credential || u.Email == credential)
            .SingleOrDefaultAsync();

        if (user == null || !Crypt.VerifyPassword(password, user.Password))
        {
            return null!;
        }

        return user;
    }

    public Task UpdateAsync(UserDto entity)
    {
        throw new NotImplementedException();
    }
}