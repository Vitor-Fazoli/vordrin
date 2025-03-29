using Domain.Interfaces;

namespace Domain.Entities.Attributes;

public class Cooldown(double duration) : IAttribute<double>
{
    private DateTime _finalTime = DateTime.MinValue;
    private readonly double _duration = Validate(duration);

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

    public void Set(double attribute)
    {
        throw new NotImplementedException();
    }
}