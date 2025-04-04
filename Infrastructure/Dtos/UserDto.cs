using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Dtos;

[Index(nameof(Username), IsUnique = true), Index(nameof(Email), IsUnique = true)]
public class UserDto
{
    [Key]
    public Guid Id { get; private set; } = new();

    [MaxLength(30)]
    [MinLength(5)]
    [Required]
    public required string Username { get; set; }

    [EmailAddress]
    [Required]
    public required string Email { get; set; }

    [MinLength(10)]
    [PasswordPropertyText]
    public required string Password { get; set; }
    public List<CharacterDto> Characters { get; private set; } = [];
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
}