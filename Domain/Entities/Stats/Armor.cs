using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities.Stats;

public class Armor(float defense) : IStat<float>
{
    private float _defense = Validate(defense);

    public StatType Type => StatType.Armor;

    private static float Validate(float defense)
    {
        if (defense < 0f)
            throw new ArgumentException("it's impossible to have less than 0 of defense");

        return defense;
    }

    public float Get()
    {
        return _defense;
    }

    public void Set(float defense)
    {
        _defense = Validate(_defense);
    }
}