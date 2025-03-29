using Domain.Interfaces;

namespace Domain.Entities.Attributes;

public class Damage(float damage, float criticalChance, float criticalMultiplier) : IAttribute<float>
{
    private float _damage = Validate(damage);
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

    public float Critical()
    {
        return Get() * CriticalMultiplier.Get();
    }
}