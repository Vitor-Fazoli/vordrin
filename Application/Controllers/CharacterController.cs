using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Interfaces;
using Infrastructure.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CharacterController(ICharacterRepository<CharacterDto> characterRepository) : ControllerBase
{
    private readonly ICharacterRepository<CharacterDto> _characterRepository = characterRepository;

    [HttpGet]
    public IActionResult GetCharacter()
    {
        var character = new
        {
            Name = "Hero",
            Level = 1,
            Attributes = new
            {
                Strength = 10,
                Agility = 8,
                Intelligence = 7
            }
        };

        return Ok(character);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCharacter([FromBody] CharacterDto character)
    {
        if (character == null)
            return BadRequest("Character data is required.");


        if (User is null)
        {
            return Unauthorized("User not authenticated!");
        }

        try
        {
            Guid ownerId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
            character.SetOwner(ownerId);

            await _characterRepository.AddAsync(character);
            return Ok(character);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}