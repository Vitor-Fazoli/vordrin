using Domain.Constants;
using Domain.Entities.Attributes;

namespace Domain.Entities
{
    public class Weapon(string name, Damage damage, WeaponClass weaponClass)
    {
        public long Id { get; set; }
        public string Name { get; set; } = name;
        public WeaponClass Class { get; set; } = weaponClass;
        public Damage Damage { get; set; } = damage;
        public int Value { get; set; } = 2;
    }
}