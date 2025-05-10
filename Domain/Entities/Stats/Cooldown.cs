using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities.Stats;

public class Cooldown(double duration) : IStat<double>
{
    private DateTime _finalTime = DateTime.MinValue;
    private readonly double _duration = Validate(duration);

    public StatType Type => StatType.Cooldown;

    public bool IsActive => DateTime.Now < _finalTime;

    public void Start()
    {
        if (IsActive) return;
        _finalTime = DateTime.Now.AddSeconds(_duration);
    }

    public static double Validate(double duration)
    {
        if (duration < 0.5f)
        {
            return 0.5f;
        }

        return duration;
    }

    public double Get()
    {
        return _duration;
    }

    public void Set(double stat)
    {
        throw new NotImplementedException();
    }
}