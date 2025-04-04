using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dtos;

public class CharacterDto
{
    [Key]
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public int Level { get; set; }
    public float Health { get; set; }
    public float Defense { get; set; }
    public float CriticalResistence { get; set; }
    public DateTime CreatedAt { get; set; }
    public WeaponDto? Weapon { get; set; }

    public CharacterDto() { }

    public CharacterDto(Guid id, string username, int level, float health, float defense, float criticalResistence, DateTime createdAt, WeaponDto? weapon)
    {
        Id = id;
        Username = username;
        Level = level;
        Health = health;
        Defense = defense;
        CriticalResistence = criticalResistence;
        CreatedAt = createdAt;
        Weapon = weapon;
    }
}