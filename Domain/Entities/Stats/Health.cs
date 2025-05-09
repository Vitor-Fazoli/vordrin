using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities.Attributes;

public class Health(float healthMax) : IStat<float>
{
    private float _health = healthMax;
    private float _healthMax = healthMax;

    public StatType Type => StatType.Health;

    public float Get()
    {
        return _health;
    }
    public float GetMax()
    {
        return _healthMax;
    }

    public bool IsFull()
    {
        return _health == _healthMax;
    }

    public bool IsEmpty()
    {
        return _health is 0;
    }

    public void Set(float health)
    {
        if (health < 0)
            _health = 0;

        if (health > _healthMax)
            _health = _healthMax;

        _health = health;
    }

    public void Decrease(float amount)
    {
        Set(_health -= amount);
    }

    public void Increase(float amount)
    {
        Set(_health += amount);
    }

    public void SetMax(float healthMax)
    {
        _healthMax = healthMax;
    }

    public static float Validate(float attribute)
    {
        throw new NotImplementedException();
    }
}