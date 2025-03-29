namespace Domain.Entities;

public class Experience
{
    public const int XP_TO_LEVEL_UP = 300;
    private int _experience = 0;

    public void Increase()
    {
        _experience++;
    }

    public void TurnZero()
    {
        _experience = 0;
    }

    public bool IsFull() => _experience == XP_TO_LEVEL_UP;
}