using Domain.Entities;
using Domain.Enums;

namespace Domain.Entities.Stats;

public class StatsCollection
{
    private readonly Dictionary<StatType, Stat> _stats = [];

    public StatsCollection()
    {
        // Inicializa com os stats padrão
        InitializeDefaultStats();
    }

    private void InitializeDefaultStats()
    {
        // Inicializa todos os stats possíveis com valores padrão
        foreach (StatType statType in Enum.GetValues<StatType>())
        {
            _stats[statType] = new Stat(statType.ToString(), GetDefaultValue(statType));
        }
    }

    private static float GetDefaultValue(StatType statType)
    {
        return statType switch
        {
            StatType.Health => 100,
            StatType.Damage => 1,
            StatType.Armor => 5,
            StatType.AbilityPower => 10,
            StatType.AbilityHaste => 5,
            StatType.CriticalChance => 0.05f,
            StatType.CriticalMultiplier => 1.5f,
            _ => 0,
        };
    }

    public Stat? GetStat(StatType statType)
    {
        if (_stats.TryGetValue(statType, out var stat))
        {
            return stat;
        }

        return null;
    }

    public float GetStatValue(StatType statType)
    {
        var stat = GetStat(statType);
        return stat?.TotalValue ?? 0;
    }

    public void SetBaseStat(StatType statType, float baseValue)
    {
        if (_stats.TryGetValue(statType, out var stat))
        {
            stat.SetBaseValue(baseValue);
        }
        else
        {
            _stats[statType] = new Stat(statType.ToString(), baseValue);
        }
    }

    public void ResetAllBonuses()
    {
        foreach (var stat in _stats.Values)
        {
            stat.ResetBonuses();
        }
    }

    public Dictionary<StatType, Stat> GetAllStats()
    {
        return _stats;
    }
}