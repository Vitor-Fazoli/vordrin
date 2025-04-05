using System.Threading.Tasks;
using Domain.Interfaces;
using Infrastructure.Dtos;

namespace Application.Services;

public class CharacterService(ICharacterRepository<CharacterDto> characterRepository)
{
    private readonly ICharacterRepository<CharacterDto> _characterRepository = characterRepository;

    public async Task<CharacterDto> GetCharacter(Guid id, Guid ownerId)
    {
        return await _characterRepository.GetByIdAsync(ownerId, id);
    }
    public async Task CreateCharacter(CharacterDto character)
    {
        await _characterRepository.AddAsync(character);
    }

    public async Task<List<CharacterDto>> GetAllByOwner(Guid ownerId)
    {
        return await _characterRepository.GetAllByOwnerAsync(ownerId);
    }
}