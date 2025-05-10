using Domain.Enums;
using Vordrin.Domain.Enums;

namespace Vordrin.Domain.Entities.Buffs;

public class DoTBuff(string name, string description, int duration,
    BuffTarget target, float damagePerTurn, DamageType damageType,
    Action<float> damageCallback) : Buff(name, description, duration, BuffType.Debuff, target, null)
{
    public float DamagePerTurn { get; private set; } = damagePerTurn;
    public DamageType DamageType { get; private set; } = damageType;
    public Action<float> DamageCallback { get; private set; } = damageCallback;

    public override bool DecreaseTurn()
    {
        if (RemainingTurns > 0)
        {
            // Aplicar dano antes de diminuir o turno
            DamageCallback?.Invoke(DamagePerTurn);
            RemainingTurns--;
        }

        return RemainingTurns <= 0;
    }
}