using Domain.Entities.Attributes;
using Domain.Interfaces;

namespace Domain.Entities;

public class Enemy : IHealable, IDamageable
{
    public Guid Id = new();
    public required Damage Damage { get; set; }
    private readonly Health Health = new(100);
    public Armor Defense { get; set; } = new(0);
    public CriticalResistence CriticalResistence { get; set; } = new(0.05f);
    public required List<Action> Skills { get; set; }
    public required Cooldown Cooldown { get; set; }
    public required Dictionary<Guid, Aggro> Aggro { get; set; }

    public void Strike(IDamageable damageable)
    {
        Random random = new();

        if (random.Next(1) > 1)
        {
            damageable.TakeDamage(Damage ?? new Damage(0.5f, 0.05f, 1.5f));
        }
        else
        {
            UseAnySkill();
        }
    }

    public void UseAnySkill()
    {
        Random random = new();

        if (Cooldown.IsActive)
            return;

        Skills[random.Next(Skills.Count - 1)].Invoke();
        Cooldown.Start();
    }

    public void ReceiveHeal(Heal heal)
    {
        Random random = new();

        if (heal.CriticalChance.Get() >= (float)random.NextDouble())
            Health.Decrease(heal.Critical());

        Health.Decrease(heal.Get());
    }

    public void TakeDamage(Damage damage)
    {
        Random random = new();

        if (damage.CriticalChance.Get() >= (float)random.NextDouble())
            Health.Decrease(damage.Critical() - Defense.Get());

        Health.Decrease(damage.Get() - Defense.Get());
    }

    public Guid GetId()
    {
        return Id;
    }

    public float GetHealth()
    {
        return Health.Get();
    }
}