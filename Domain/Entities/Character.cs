using System.ComponentModel.DataAnnotations;
using Domain.Entities.Attributes;
using Domain.Entities.Attributes.PrimaryAttributes;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Character(string name, Weapon weapon, PrimaryAttributes primaryAttributes) : IDamageable, IHealable
    {
        [Key]
        public Guid Id = new();
        public required User User { get; set; }
        public Username Username = new(name);
        private readonly Level _level = new() { experience = new() };
        private Weapon Weapon { get; set; } = weapon;
        public readonly Health _health = new(100);
        public readonly Defense _defense = new(0);
        public readonly CriticalResistence _criticalResistence = new(0.05f);
        public DateTime CreatedAt = DateTime.Now;
        public PrimaryAttributes PrimaryAttributes { get; set; } = primaryAttributes;

        public void Strike(IDamageable damageable)
        {
            damageable.TakeDamage(Weapon?.Damage ?? new Damage(0.5f, 0.05f, 1.5f));
        }
        public void ReceiveHeal(Heal heal)
        {
            Random random = new();

            if (heal.CriticalChance.Get() >= (float)random.NextDouble())
                _health.Decrease(heal.Critical());

            _health.Decrease(heal.Get());
        }

        public void TakeDamage(Damage damage)
        {
            Random random = new();

            if (damage.CriticalChance.Get() >= (float)random.NextDouble())
                _health.Decrease(damage.Critical() - _defense.Get());

            _health.Decrease(damage.Get() - _defense.Get());
        }

        public Guid GetId()
        {
            return Id;
        }
    }
}