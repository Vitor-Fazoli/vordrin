namespace Domain.Entities;

public class WeaponEffect
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public float ChanceToTrigger { get; private set; }
    public float Duration { get; private set; }
    public int Cooldown { get; private set; }
    public bool IsOnCooldown { get; private set; }
    public float CooldownRemaining { get; private set; }

    public delegate void EffectAction(Character user, Enemy target, Weapon weapon);
    private readonly EffectAction OnTriggerAction;

    public WeaponEffect(string name, string description, float chanceToTrigger, float duration, int cooldown, EffectAction effectAction)
    {
        Name = name;
        Description = description;
        ChanceToTrigger = chanceToTrigger;
        Duration = duration;
        Cooldown = cooldown;
        IsOnCooldown = false;
        CooldownRemaining = 0;
        OnTriggerAction = effectAction;
    }

    public bool TryTrigger(Character user, Enemy target, Weapon weapon)
    {
        if (IsOnCooldown)
            return false;

        if (Random.Shared.NextDouble() <= ChanceToTrigger)
        {
            OnTriggerAction?.Invoke(user, target, weapon);
            IsOnCooldown = true;
            CooldownRemaining = Cooldown;
            return true;
        }

        return false;
    }

    public void Update(float deltaTime)
    {
        if (IsOnCooldown)
        {
            CooldownRemaining -= deltaTime;
            if (CooldownRemaining <= 0)
            {
                IsOnCooldown = false;
                CooldownRemaining = 0;
            }
        }
    }
}