using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities.Attributes;

public class AbilityPower(float abilityPower) : IStat<float>
{
    private float _abilityPower = abilityPower;
    public StatType Type => StatType.AbilityPower;

    public float Get()
    {
        return _abilityPower;
    }

    public void Set(float attribute)
    {
        _abilityPower = attribute;
    }
}