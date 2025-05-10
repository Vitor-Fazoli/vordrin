using Domain.Entities.Stats;
using Domain.Enums;

namespace Vordrin.Domain.Entities.Buffs
{
    public abstract class Buff(string name, string description, int duration, BuffType buffType, BuffTarget target, BuffEffect? effect)
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; } = name;
        public string Description { get; protected set; } = description;
        public int Duration { get; protected set; } = duration;
        public int RemainingTurns { get; protected set; } = duration;
        public BuffType BuffType { get; protected set; } = buffType;
        public BuffTarget Target { get; protected set; } = target;
        public bool IsActive => RemainingTurns > 0;
        public BuffEffect Effect { get; protected set; } = effect;


        public virtual void Apply(StatsCollection stats)
        {
            Effect.Apply(stats);
        }

        public virtual void Remove(StatsCollection stats)
        {
            Effect.Remove(stats);
        }

        public virtual bool DecreaseTurn()
        {
            if (RemainingTurns > 0)
            {
                RemainingTurns--;
            }

            return RemainingTurns <= 0;
        }

        public virtual void Refresh()
        {
            RemainingTurns = Duration;
        }

        public virtual void ExtendDuration(int additionalTurns)
        {
            RemainingTurns += additionalTurns;
        }
    }
}