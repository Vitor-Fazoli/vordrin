using Domain.Entities.Attributes;

namespace Domain.Interfaces;

public interface IDamageable
{
    public void TakeDamage(Damage damage);
}