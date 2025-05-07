using Domain.Interfaces;

namespace Domain.Entities.Attributes;

public class AbilityPower(float abilityPower) : IAttribute<float>
{
    private float _abilityPower = abilityPower;
    public float Get()
    {
        return _abilityPower;
    }

    public void Set(float attribute)
    {
        throw new NotImplementedException();
    }
}