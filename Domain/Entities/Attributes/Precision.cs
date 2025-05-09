using Domain.Interfaces;

namespace Domain.Entities.Attributes;

public class Precision(int precision) : IAttribute
{
    private int _precision = precision;

    public int Get()
    {
        return _precision;
    }

    public void Set(int precision)
    {
        if (precision < 1)
            _precision = 1;

        // This is necessary because precision is summed with other attributes to total 15;
        if (precision > 11)
            _precision = 11;

        _precision = precision;
    }
}