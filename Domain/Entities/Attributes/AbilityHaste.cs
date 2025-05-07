using Domain.Interfaces;

namespace Domain.Entities.Attributes;

public class AbilityHaste(float abilityHaste) : IAttribute<float>
{

    private float _abilityHaste = abilityHaste;
    public float Get()
    {
        return _abilityHaste;
    }

    public void Set(float attribute)
    {
        throw new NotImplementedException();
    }
}