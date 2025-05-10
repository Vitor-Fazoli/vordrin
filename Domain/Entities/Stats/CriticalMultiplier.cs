using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities.Stats;

public class CriticalMultiplier(float criticalMultiplier) : IStat<float>
{
    private float _criticalMultiplier = Validate(criticalMultiplier);

    public StatType Type => StatType.CriticalMultiplier;

    private static float Validate(float criticalMultiplier)
    {
        if (criticalMultiplier >= 3.0f)
            return 3f;

        if (criticalMultiplier < 1f)
            return 1f;

        return criticalMultiplier;
    }

    public float Get()
    {
        return _criticalMultiplier;
    }

    public void Set(float criticalMultiplier)
    {
        _criticalMultiplier = Validate(criticalMultiplier);
    }
}