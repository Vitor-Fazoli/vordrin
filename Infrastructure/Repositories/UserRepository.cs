using System.Data.Common;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(VordrinDbContext db) : IRepository<User>
{
    public async Task AddAsync(User entity)
    {
        try
        {
            await db.Users.AddAsync(entity);
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
            User user = db.Users.Where(u => u.Id == id).Single();

            db.Users.Remove(user);
            await db.SaveChangesAsync();
        }
        catch (DbException)
        {
            throw;
        }
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        throw new Exception("Not Authorized");
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await db.Users.Where(u => u.Id == id).SingleAsync();
    }

    public async Task UpdateAsync(User entity)
    {
        try
        {
            db.Users.Update(entity);
            await db.SaveChangesAsync();
        }
        catch (DbException)
        {
            throw;
        }
    }
}