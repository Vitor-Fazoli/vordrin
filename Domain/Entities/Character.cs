using System.ComponentModel.DataAnnotations;
using Domain.Entities.Attributes;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Character(string name, Weapon weapon, CharacterAttributes attributes) : IDamageable, IHealable
    {
        [Key]
        public Guid Id = new();
        public required User User { get; set; }
        public Username Username = new(name);
        public Level Level { get; private set; } = new() { experience = new() };
        private Weapon Weapon { get; set; } = weapon;
        public DateTime CreatedAt = DateTime.Now;
        public CharacterAttributes Attributes { get; set; } = attributes;

        public void Strike(IDamageable damageable)
        {
            damageable.TakeDamage(Weapon?.Damage ?? new Damage(0.5f, 0.05f, 1.5f));
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

        public Guid GetId()
        {
            return Id;
        }
    }
}