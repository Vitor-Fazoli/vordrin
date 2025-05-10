using Domain.Enums;

namespace Vordrin.Domain.Entities.Buffs;

public class HoTBuff(string name, string description, int duration,
    BuffTarget target, float healingPerTurn,
    Action<float> healingCallback) : Buff(name, description, duration, BuffType.Buff, target, null)
{
    public float HealingPerTurn { get; private set; } = healingPerTurn;
    public Action<float> HealingCallback { get; private set; } = healingCallback;

    public override bool DecreaseTurn()
    {
        if (RemainingTurns > 0)
        {
            // Aplicar cura antes de diminuir o turno
            HealingCallback?.Invoke(HealingPerTurn);
            RemainingTurns--;
        }

        return RemainingTurns <= 0;
    }
}