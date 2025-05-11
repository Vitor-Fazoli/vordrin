namespace Domain.Entities;

public class Level()
{
    public int _level = 0;
    public DateTime updatedAt = DateTime.Now;
    public required ExperiencePoints experience;

    public void LevelUp()
    {
        if (DateTime.Now < updatedAt.AddMinutes(1))
            throw new Exception("Sorry, you already leveled up recently. Please wait before leveling up again.");

        if (!experience.IsFull())
            throw new Exception("You don't have enough experience.");

        _level += 1;
        experience.TurnZero();
        updatedAt = DateTime.Now;
    }

    public void IncreaseExperience()
    {
        experience.Increase();

        if (experience.IsFull())
            LevelUp();
    }

    public int GetLevel()
    {
        return _level;
    }
}