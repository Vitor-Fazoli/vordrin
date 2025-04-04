using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dtos;

public class WeaponDto
{
    [Key]
    public Guid Id { get; set; } = new();
    public string Name { get; set; } = string.Empty;
    public float Damage { get; set; }
    public float CriticalChance { get; set; }
    public float CriticalMultiplier { get; set; }

    public WeaponDto()
    {
    }

    public WeaponDto(string name, float damage, float criticalChance, float criticalMultiplier)
    {
        Name = name;
        Damage = damage;
        CriticalChance = criticalChance;
        CriticalMultiplier = criticalMultiplier;
    }
}