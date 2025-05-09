using Domain.Entities;
using Domain.Entities.Attributes;
using Domain.Enums;
using Domain.Interfaces;

public abstract class WeaponBase : IWeapon
{
    public string Id { get; protected set; }
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public WeaponType Type { get; protected set; }
    public Damage BaseDamage { get; protected set; }
    public int Level { get; protected set; }
    public Dictionary<string, IStat<float>> Stats { get; protected set; } = [];

    public WeaponBase(string id, string name, string description, WeaponType type,
                        Damage baseDamage, float attackSpeed, int level)
    {
        Id = id;
        Name = name;
        Description = description;
        Type = type;
        BaseDamage = baseDamage;
        Level = level;
    }

    public abstract void OnLeftClick(Character user, Enemy target);
    public abstract void OnRightClick(Character user);
    public abstract void Update(float deltaTime);

    protected float CalculateDamage()
    {
        return BaseDamage.Get() + Stats.Values.Sum(stat => stat.Type == StatType.Damage ? stat.Get() : 0);
    }
}