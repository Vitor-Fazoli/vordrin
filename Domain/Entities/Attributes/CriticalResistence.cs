using Domain.Interfaces;

namespace Domain.Entities.Attributes;

public class CriticalResistence(float criticalResitence) : IAttribute<float>
{
    private float _criticalResistence = Validate(criticalResitence);
    private static float Validate(float criticalResistence)
    {
        if (criticalResistence >= 1.0f)
            return 1f;

        if (criticalResistence < 0f)
            return 0f;

        return criticalResistence;
    }

    public float Get()
    {
        return _criticalResistence;
    }

    public void Set(float criticalResistence)
    {
        _criticalResistence = Validate(criticalResistence);
    }
}