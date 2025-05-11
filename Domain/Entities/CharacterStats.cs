using Domain.Entities.Stats;

namespace Domain.Entities;

public class CharacterStats
{
    // Stats básicas
    public Health Health { get; private set; }
    public Armor Defense { get; private set; }

    // Stats críticas
    public CriticalChance CriticalChance { get; private set; }
    public CriticalMultiplier CriticalMultiplier { get; private set; }
    public CriticalResistence CriticalResistence { get; private set; }

    public AbilityHaste AbilityHaste { get; private set; }
    public AbilityPower AbilityPower { get; private set; }
    public Damage BaseDamage { get; private set; }

    public CharacterStats(CharacterAttributes attributes) => CalculateBaseStats(attributes);

    public void CalculateBaseStats(CharacterAttributes attributes)
    {
        // Defensive stats
        Health = new Health(100 + (attributes.Vigor.Get() * 2));
        Defense = new Armor(0);
        CriticalResistence = new CriticalResistence(attributes.Vigor.Get() * 0.01f);

        // Ability stats
        AbilityHaste = new AbilityHaste(attributes.Rhythm.Get() * 0.05f);
        AbilityPower = new AbilityPower(attributes.Wisdom.Get() * 0.05f);

        // Ofensive stats
        BaseDamage = new Damage(attributes.Ferocity.Get() * 5, CriticalChance.Get(), CriticalMultiplier.Get());
        CriticalChance = new CriticalChance(0.05f + (attributes.Precision.Get() * 0.01f));
        CriticalMultiplier = new CriticalMultiplier(1.5f + (attributes.Ferocity.Get() * 0.05f));
    }

    public void RecalculateWithWeapon(Weapon weapon)
    {
        BaseDamage = new Damage(BaseDamage.Get() + weapon.BaseDamage.Get(),
                                CriticalChance.Get() + weapon.BaseDamage.CriticalChance.Get(),
                                CriticalMultiplier.Get());

        ApplyWeaponEffects(weapon);
    }

    private void ApplyWeaponEffects(Weapon weapon)
    {
        // Lógica para aplicar efeitos especiais da arma
        foreach (var effect in weapon.Effects)
        {
            ApplyStatEffect(effect);
        }
    }

    private void ApplyStatEffect(StatEffect effect)
    {
        // Aplicar efeito às estatísticas apropriadas
        switch (effect.Type)
        {
            case StatEffectType.HealthBoost:
                Health = new Health(Health.Maximum + effect.Value);
                break;
            case StatEffectType.ManaBoost:
                Mana = new Mana(Mana.Maximum + effect.Value);
                break;
            case StatEffectType.DefenseBoost:
                Defense = new Armor(Defense.Get() + effect.Value);
                break;
                // Adicionar mais tipos de efeitos conforme necessário
        }
    }

    public void ApplyEquipmentBonuses(Equipment equipment)
    {
        // Aplicar bônus de equipamentos
        foreach (var item in equipment.GetEquippedItems())
        {
            foreach (var bonus in item.StatBonuses)
            {
                ApplyStatBonus(bonus);
            }
        }
    }

    private void ApplyStatBonus(StatBonus bonus)
    {
        // Aplicar bônus às estatísticas apropriadas
        switch (bonus.Type)
        {
            case StatBonusType.Health:
                Health = new Health(Health.Maximum + bonus.Value);
                break;
            case StatBonusType.Mana:
                Mana = new Mana(Mana.Maximum + bonus.Value);
                break;
            case StatBonusType.Defense:
                Defense = new Armor(Defense.Get() + bonus.Value);
                break;
                // Outros tipos de bônus
        }
    }
}