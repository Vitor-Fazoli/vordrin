using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities.Attributes;

public class AbilityHaste(float abilityHaste) : IStat<float>
{

    private float _abilityHaste = abilityHaste;
    public StatType Type => StatType.AbilityHaste;

    public float Get()
    {
        return _abilityHaste;
    }

    public void Set(float attribute)
    {
        _abilityHaste = attribute;
    }
}