using System.ComponentModel.DataAnnotations;
using Domain.Entities.Attributes.PrimaryAttributes;

namespace Infrastructure.Dtos;

public class CharacterDto
{
    [Key]
    public Guid Id { get; set; }
    public Guid Owner { get; private set; }
    public string Username { get; set; } = string.Empty;
    public int Level { get; private set; } = 1;
    public int Experience { get; set; } = 0;
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public WeaponDto? Weapon { get; set; }
    public int Ferocity { get; set; }
    public int Precision { get; set; }
    public int Rhythm { get; set; }
    public int Vigor { get; set; }
    public int Wisdom { get; set; }

    public CharacterDto() { }

    public CharacterDto(Guid id, Guid owner, string username, int level, int experience, float health, float defense, float criticalResistence, DateTime createdAt, WeaponDto? weapon, int ferocity, int precision, int rhythm, int vigor, int wisdom)
    {
        Id = id;
        Owner = owner;
        Username = username;
        Weapon = weapon;
        Ferocity = ferocity;
        Precision = precision;
        Rhythm = rhythm;
        Vigor = vigor;
        Wisdom = wisdom;
    }

    public void SetOwner(Guid owner) => Owner = owner;
}