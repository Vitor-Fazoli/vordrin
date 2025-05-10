
using Domain.Entities.Stats;
using Domain.Enums;
using Vordrin.Domain.Entities.Buffs;

namespace Domain.Entities.Buffs;

public class StackableBuff(string name, string description, int duration, BuffType buffType,
    BuffTarget target, BuffEffect effect, int maxStacks) : Buff(name, description, duration, buffType, target, effect)
{
    public int MaxStacks { get; private set; } = maxStacks;
    public int CurrentStacks { get; private set; } = 1;

    public bool AddStack()
    {
        if (CurrentStacks < MaxStacks)
        {
            CurrentStacks++;
            return true;
        }

        return false;
    }

    public override void Apply(StatsCollection stats)
    {
        for (int i = 0; i < CurrentStacks; i++)
        {
            Effect.Apply(stats);
        }
    }

    public override void Remove(StatsCollection stats)
    {
        for (int i = 0; i < CurrentStacks; i++)
        {
            Effect.Remove(stats);
        }
    }
}