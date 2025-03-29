using Domain.Interfaces;

namespace Domain.Entities.Attributes;

public class Heal(float heal, float criticalChance, float criticalMultiplier) : IAttribute<float>
{
    private float _heal = Validate(heal);
    public CriticalChance CriticalChance { get; set; } = new(criticalChance);
    public CriticalMultiplier CriticalMultiplier { get; set; } = new(criticalMultiplier);

    private static float Validate(float heal)
    {
        if (heal < 0)
            return 0;

        return heal;
    }
    public float Get()
    {
        return _heal;
    }

    public void Set(float heal)
    {
        _heal = Validate(heal);
    }

    public float Critical()
    {
        return Get() * CriticalMultiplier.Get();
    }
}