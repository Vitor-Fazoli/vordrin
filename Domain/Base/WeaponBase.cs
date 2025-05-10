using Domain.Entities;
using Domain.Entities.Attributes;
using Domain.Entities.Stats;
using Domain.Enums;
using Domain.Interfaces;

public abstract class WeaponBase(string id, string name, string description, WeaponType type,
                    Damage baseDamage, int level) : IWeapon
{
    public string Id { get; protected set; } = id;
    public string Name { get; protected set; } = name;
    public string Description { get; protected set; } = description;
    public WeaponType Type { get; protected set; } = type;
    public Damage BaseDamage { get; protected set; } = baseDamage;
    public int Level { get; protected set; } = level;
    public StatsCollection Stats { get; protected set; } = new StatsCollection();
    public abstract void OnLeftClick(Character user, Enemy target);
    public abstract void OnRightClick(Character user);
    public abstract void Update(float deltaTime);

    public Damage CalculateDamage()
    {
        return new Damage(BaseDamage.Get() + Stats.Values.Sum(stat => stat.Type is StatType.Damage ? stat.Get() : 0),
        BaseDamage.CriticalChance.Get() + Stats.Values.Sum(stat => stat.Type is StatType.CriticalChance ? stat.Get() : 0),
        BaseDamage.CriticalMultiplier.Get() + Stats.Values.Sum(stat => stat.Type is StatType.CriticalMultiplier ? stat.Get() : 0));
    }

    
}