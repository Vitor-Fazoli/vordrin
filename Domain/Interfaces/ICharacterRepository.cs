namespace Domain.Interfaces;

public interface ICharacterRepository<CharacterDto>
{
    Task<List<CharacterDto>> GetAllByOwnerAsync(Guid ownerId);
    Task<CharacterDto> GetByIdAsync(Guid id, Guid ownerId);
    Task AddAsync(CharacterDto entity);
    Task UpdateAsync(CharacterDto entity);
    Task DeleteAsync(Guid id, Guid ownerId);
}