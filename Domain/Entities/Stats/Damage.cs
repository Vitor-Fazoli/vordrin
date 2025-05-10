using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities.Stats;

public class Damage(float damage, float criticalChance, float criticalMultiplier) : IStat<float>
{
    private float _damage = Validate(damage);

    public StatType Type => StatType.Damage;
    public CriticalChance CriticalChance { get; set; } = new(criticalChance);
    public CriticalMultiplier CriticalMultiplier { get; set; } = new(criticalMultiplier);


    private static float Validate(float damage)
    {
        if (damage < 0)
            return 0;

        return damage;
    }

    public float Get()
    {
        return _damage;
    }

    public void Set(float damage)
    {
        _damage = Validate(damage);
    }

    public Damage Add(float damage)
    {
        return new Damage(_damage * damage, CriticalChance.Get(), CriticalMultiplier.Get());
    }

    public Damage Multiply(float multiplier)
    {
        return new Damage(_damage * multiplier, CriticalChance.Get(), CriticalMultiplier.Get());
    }

    public float Critical()
    {
        return Get() * CriticalMultiplier.Get();
    }
}