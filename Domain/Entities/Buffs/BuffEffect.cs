using Domain.Entities.Stats;
using Domain.Enums;
using Vordrin.Domain.Enums;

namespace Vordrin.Domain.Entities.Buffs;

public class BuffEffect(StatType statType, float value, EffectType effectType)
{
    public StatType StatType { get; private set; } = statType;
    public float Value { get; private set; } = value;
    public EffectType EffectType { get; private set; } = effectType;

    public void Apply(StatsCollection stats)
    {
        var stat = stats.GetStat(StatType);
        if (stat == null) return;

        switch (EffectType)
        {
            case EffectType.FlatBonus:
                stat.AddFlatBonus(Value);
                break;
            case EffectType.PercentageBonus:
                stat.AddPercentageBonus(Value);
                break;
            case EffectType.FlatReduction:
                stat.AddFlatBonus(-Value);
                break;
            case EffectType.PercentageReduction:
                stat.AddPercentageBonus(-Value);
                break;
            case EffectType.StatusEffect:
                // Implementar lógica de efeitos de status como stun, silence, etc.
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Remove(StatsCollection stats)
    {
        var stat = stats.GetStat(StatType);
        if (stat == null) return;

        switch (EffectType)
        {
            case EffectType.FlatBonus:
                stat.RemoveFlatBonus(Value);
                break;
            case EffectType.PercentageBonus:
                stat.RemovePercentageBonus(Value);
                break;
            case EffectType.FlatReduction:
                stat.RemoveFlatBonus(-Value);
                break;
            case EffectType.PercentageReduction:
                stat.RemovePercentageBonus(-Value);
                break;
            case EffectType.StatusEffect:
                // Implementar lógica de remoção de efeitos de status
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}