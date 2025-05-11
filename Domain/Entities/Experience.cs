namespace Domain.Entities;

public class ExperiencePoints
{
    public int Current { get; private set; }
    public int NextLevelThreshold { get; private set; }
    public int Level { get; private set; }

    public ExperiencePoints()
    {
        Level = 1;
        Current = 0;
        CalculateNextLevelThreshold();
    }

    private void CalculateNextLevelThreshold()
    {
        NextLevelThreshold = 100;
    }

    public void Add(int amount)
    {
        if (amount <= 0) return;

        Current += amount;
    }

    public bool CanLevelUp()
    {
        return Current >= NextLevelThreshold;
    }

    public void LevelUp()
    {
        if (!CanLevelUp()) return;

        Current -= NextLevelThreshold;
        Level++;
        CalculateNextLevelThreshold();
    }
}