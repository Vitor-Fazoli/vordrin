namespace Vordrin.Domain.Enums
{
    public enum EffectType
    {
        FlatBonus,           // Bônus fixo (ex: +5 ATK)
        PercentageBonus,     // Bônus percentual (ex: +10% ATK)
        FlatReduction,       // Redução fixa (ex: -5 DEF)
        PercentageReduction, // Redução percentual (ex: -10% DEF)
        StatusEffect         // Efeito de status (stun, silence, etc)
    }
}