using Domain.Entities.Attributes;
using Domain.Entities.Stats;
using Domain.Enums;
using Vordrin.Domain.Entities.Buffs;
using Vordrin.Domain.Enums;

namespace Vordrin.Domain.Entities;

public abstract class Alived
{
    public string Name { get; protected set; }
    public int Level { get; protected set; }
    public StatsCollection Stats { get; protected set; }
    public BuffManager BuffManager { get; protected set; }

    protected float _currentHealth;
    public float CurrentHealth
    {
        get => _currentHealth;
        protected set => _currentHealth = Math.Min(value, Stats.GetStatValue(StatType.Health));
    }

    protected Alived(string name, int level)
    {
        Name = name;
        Level = level;
        Stats = new StatsCollection();
        BuffManager = new BuffManager(Stats);
        _currentHealth = Stats.GetStatValue(StatType.Health);
    }

    public virtual void TakeDamage(float damage, DamageType damageType = DamageType.Physical)
    {
        float reducedDamage = CalculateReducedDamage(damage, damageType);
        CurrentHealth -= reducedDamage;
    }

    public virtual void Heal(float amount)
    {
        CurrentHealth += amount;
    }

    protected virtual float CalculateReducedDamage(float damage, DamageType damageType)
    {
        var armor = damageType switch
        {
            _ => Stats.GetStatValue(StatType.Armor),
        };

        return damage / (1 + armor / 100);
    }

    public virtual void ProcessTurn()
    {
        // Processa todos os buffs ativos
        BuffManager.ProcessTurn();
    }

    public virtual bool IsAlive()
    {
        return CurrentHealth > 0;
    }

    public virtual void AddBuff(Buff buff)
    {
        BuffManager.AddBuff(buff);
    }

    public virtual void RemoveBuff(Guid buffId)
    {
        BuffManager.RemoveBuff(buffId);
    }
}

