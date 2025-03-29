using Domain.Interfaces;

namespace Domain.Entities.Attributes;

public class CriticalChance(float criticalChance) : IAttribute<float>
{
    private float _criticalChance = Validate(criticalChance);

    private static float Validate(float criticalChance)
    {
        if (criticalChance >= 1.0f)
            throw new ArgumentException("it's impossible to have more than 100% of critical chance");

        if (criticalChance < 0f)
            throw new ArgumentException("it's impossible to have less than 0% of critical chance");

        return criticalChance;
    }

    public float Get()
    {
        return _criticalChance;
    }

    public void Set(float criticalChance)
    {
        _criticalChance = Validate(criticalChance);
    }
}