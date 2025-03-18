namespace Server.Models
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Enemy
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int HealthMax { get; set; }
    }

    public class GameState
    {
        public List<Player> Players { get; set; } = new();
        public List<Enemy> Enemies { get; set; } = new();
    }

    public class PlayerAttack
    {
        public string EnemyId { get; set; }
        public string PlayerId { get; set; }
        public long Timestamp { get; set; }
    }
}