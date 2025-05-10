using Domain.Entities;
using Domain.Entities.Attributes;
using Domain.Entities.Stats;
using Domain.Enums;

namespace Domain.Interfaces
{
    public interface IWeapon
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; }
        public WeaponType Type { get; }
        public WeaponRarity Rarity { get; }
        public Damage BaseDamage { get; }
        public int Level { get; }
        public int MaxLevel { get; }
        public StatsCollection Stats { get; }
        public List<WeaponEffect> Effects { get; }
        public void OnLeftClick(Character user, Enemy target);
        public void OnRightClick(Character user);
        public void Update(float deltaTime);

        public Damage CalculateDamage();
        public bool CanUpgrade();
        public void Upgrade();
        public float GetUpgradeCost();
        public Dictionary<string, string> GetWeaponInfo();
    }
}