using Domain.Entities;
using Domain.Entities.Attributes;
using Domain.Enums;

namespace Domain.Interfaces
{
    public interface IWeapon
    {
        string Id { get; }
        string Name { get; }
        string Description { get; }
        WeaponType Type { get; }
        Damage BaseDamage { get; }

        void OnLeftClick(Character user, Enemy target);
        void OnRightClick(Character user);
        void Update(float deltaTime);
    }
}