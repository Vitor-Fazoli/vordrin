using Domain.Interfaces;
using Infrastructure.Config;
using Infrastructure.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CharacterRepository(VordrinDbContext context) : ICharacterRepository<CharacterDto>
{
    private readonly VordrinDbContext _context = context;

    public Task AddAsync(CharacterDto entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid id, Guid ownerId)
    {
        var character = await _context.Characters
            .Where(c => c.Id == id && c.Owner == ownerId)
            .FirstOrDefaultAsync() ?? throw new Exception("Character not found");

        _context.Characters.Remove(character);
        await _context.SaveChangesAsync();
    }

    public async Task<List<CharacterDto>> GetAllByOwnerAsync(Guid ownerId)
    {
        return await _context.Characters
            .Where(c => c.Owner == ownerId)
            .ToListAsync();
    }

    public async Task<CharacterDto> GetByIdAsync(Guid id, Guid ownerId)
    {
        return await _context.Characters
            .Where(c => c.Id == id && c.Owner == ownerId)
            .FirstOrDefaultAsync() ?? throw new Exception("Character not found");
    }

    public Task UpdateAsync(CharacterDto entity)
    {
        throw new NotImplementedException();
    }
}
