using Domain.Entities.Attributes;
using Domain.Entities.Stats;
using Vordrin.Domain.Entities;

namespace Domain.Entities
{
    public class Character : Alived
    {
        public Guid Id { get; private set; }
        public required User User { get; set; }
        private Weapon Weapon { get; set; }
        public DateTime CreatedAt = DateTime.Now;
        public CharacterAttributes Attributes { get; set; }
        public CharacterStats CharacterStats { get; set; }

        public Character(string name, int level, Weapon weapon, Ferocity fer, Precision pre, Rhythm Rhy, Vigor vig, Wisdom Wis) : base(name, level)
        {
            Id = new();
            Name = new(name);
            Weapon = weapon;
            Attributes = new CharacterAttributes(fer, pre, Rhy, vig, Wis);
            CharacterStats = new CharacterStats(Attributes);
        }

        public void ReceiveHeal(Heal heal)
        {
            Random random = new();

            if (heal.CriticalChance.Get() >= (float)random.NextDouble())
                Attributes.Health.Decrease(heal.Critical());

            Attributes.Health.Decrease(heal.Get());
        }

        public void TakeDamage(Damage damage)
        {
            Random random = new();

            if (damage.CriticalChance.Get() >= (float)random.NextDouble())
                Attributes.Health.Decrease(damage.Critical() - Attributes.Defense.Get());

            Attributes.Health.Decrease(damage.Get() - Attributes.Defense.Get());
        }
    }
}