using Domain.Entities.Attributes;
using Domain.Entities.Stats;
using Domain.Enums;
using Domain.Interfaces;
using Vordrin.Domain.Enums;

namespace Domain.Entities;
public class Weapon : Item, IWeapon
{
    private readonly TimeSpan cooldown = TimeSpan.FromSeconds(0.05);
    private DateTime lastAttackTime;
    public WeaponType Type { get; protected set; }
    public WeaponRarity Rarity { get; protected set; }
    public Damage BaseDamage { get; protected set; }
    public int Level { get; protected set; }
    public int MaxLevel { get; protected set; }
    public StatsCollection Stats { get; protected set; }
    public List<WeaponEffect> Effects { get; protected set; }
    public string? SpecialAbilityName { get; protected set; }
    public string? SpecialAbilityDescription { get; protected set; }
    public float SpecialAbilityCooldown { get; protected set; }
    public float SpecialAbilityRemainingCooldown { get; protected set; }

    public Weapon(string id, string name, string description, WeaponType type, WeaponRarity rarity,
               Damage baseDamage, int level = 1, int maxLevel = 50)
        : base(id, name)
    {
        Type = type;
        Rarity = rarity;
        BaseDamage = baseDamage;
        Level = level;
        MaxLevel = maxLevel;
        Stats = new StatsCollection();
        Effects = [];

        SpecialAbilityName = "Basic Special";
        SpecialAbilityDescription = "A basic special ability";
        SpecialAbilityCooldown = 10.0f;
        SpecialAbilityRemainingCooldown = 0;

        ApplyRarityModifiers();
    }

    private bool CanAttack()
    {
        TimeSpan timeSinceLastAttack = DateTime.Now - lastAttackTime;

        return timeSinceLastAttack >= cooldown;
    }

    protected virtual void ApplyRarityModifiers()
    {
        float rarityMultiplier = 1.0f + (0.2f * (int)Rarity);
        BaseDamage = new Damage(BaseDamage.Get() * rarityMultiplier,
                            BaseDamage.CriticalChance.Get() + (0.01f * (int)Rarity),
                            BaseDamage.CriticalMultiplier.Get() + (0.05f * (int)Rarity));

        int effectSlots = Math.Max(0, (int)Rarity - 1);
    }

    public virtual Damage CalculateDamage()
    {
        float baseValue = BaseDamage.Get();
        float damageBonus = Stats.GetAllStats()
            .Where(stat => stat.Key == StatType.Damage)
            .Sum(stat => stat.Value.TotalValue);

        float critChance = BaseDamage.CriticalChance.Get() + Stats.GetAllStats()
            .Where(stat => stat.Key is StatType.CriticalChance)
            .Sum(stat => stat.Value.TotalValue);

        float critMulti = BaseDamage.CriticalMultiplier.Get() + Stats.GetAllStats()
            .Where(stat => stat.Key is StatType.CriticalMultiplier)
            .Sum(stat => stat.Value.TotalValue);

        return new Damage(baseValue + damageBonus, critChance, critMulti);
    }

    public virtual void OnLeftClick(Character user, Enemy target)
    {
        if (CanAttack() is not true)
        {
            return;
        }

        lastAttackTime = DateTime.Now;

        Damage damage = CalculateDamage();
        float finalDamage = damage.Critical();

        target.TakeDamage(finalDamage, DamageType.Physical); //! ALTERAR PARA O TIPO DE DANO CORRETO

        foreach (WeaponEffect effect in Effects)
        {
            effect.TryTrigger(user, target, this);
        }
    }

    public virtual void OnRightClick(Character user)
    {
        if (SpecialAbilityRemainingCooldown > 0)
            return;

        // Implementação da habilidade especial (a ser sobrescrita nas subclasses)
        UseSpecialAbility(user);

        // Iniciar cooldown da habilidade especial
        SpecialAbilityRemainingCooldown = SpecialAbilityCooldown;
    }

    protected virtual void UseSpecialAbility(Character user)
    {
        // Implementação básica - deve ser sobrescrita
        Console.WriteLine($"Using special ability: {SpecialAbilityName}");
    }

    public virtual void Update(float deltaTime)
    {
        // Atualizar cooldown da habilidade especial
        if (SpecialAbilityRemainingCooldown > 0)
        {
            SpecialAbilityRemainingCooldown -= deltaTime;
            if (SpecialAbilityRemainingCooldown < 0)
                SpecialAbilityRemainingCooldown = 0;
        }

        // Atualizar efeitos
        foreach (var effect in Effects)
        {
            effect.Update(deltaTime);
        }
    }

    public virtual bool CanUpgrade()
    {
        return Level < MaxLevel;
    }

    public virtual void Upgrade()
    {
        if (!CanUpgrade())
            return;

        Level++;

        // Aumentar dano base com base no nível
        BaseDamage.Add(BaseDamage.Get() * 0.1f); // 10% de aumento por nível

        // Bônus adicional a cada 10 níveis
        if (Level % 10 == 0)
        {
            // BaseDamage.CriticalChance = new CriticalChance(StatType.CriticalChance,
            //     BaseDamage.CriticalChance.Get() + 0.02f);

            // BaseDamage.CriticalMultiplier = new Stat(StatType.CriticalMultiplier,
            //     BaseDamage.CriticalMultiplier.Get() + 0.1f);
        }
    }

    public virtual float GetUpgradeCost()
    {
        return 100 * Level * (1 + (int)Rarity * 0.5f);
    }

    public Dictionary<string, string> GetWeaponInfo()
    {
        var info = new Dictionary<string, string>
            {
                { "Name", Name },
                { "Type", Type.ToString() },
                { "Rarity", Rarity.ToString() },
                { "Level", $"{Level}/{MaxLevel}" },
                { "Damage", $"{BaseDamage.Get():F1}" },
                { "Critical Chance", $"{BaseDamage.CriticalChance.Get() * 100:F1}%" },
                { "Critical Multiplier", $"{BaseDamage.CriticalMultiplier.Get():F1}x" },
                { "Special Ability", SpecialAbilityName ?? "None" },
                { "Special Ability Description", SpecialAbilityDescription ?? "None" },
                { "Special Cooldown", $"{SpecialAbilityCooldown:F1}s" }
            };

        // Adicionar informações sobre efeitos
        for (int i = 0; i < Effects.Count; i++)
        {
            info.Add($"Effect {i + 1}", Effects[i].Name);
            info.Add($"Effect {i + 1} Description", Effects[i].Description);
        }

        return info;
    }
    public static Weapon Create(string name, WeaponType type, WeaponRarity rarity, int level = 1)
    {
        // Criar dano base apropriado para o tipo e raridade
        float baseDamageValue = 10.0f * (1 + (int)type * 0.5f);
        Damage damage = new Damage(baseDamageValue);

        return new Weapon(
            Guid.NewGuid().ToString(),
            name,
            $"A {rarity.ToString().ToLower()} {type.ToString().ToLower()}",
            type,
            rarity,
            damage,
            level
        );
    }
}