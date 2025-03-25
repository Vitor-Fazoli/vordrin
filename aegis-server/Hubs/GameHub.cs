using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace aegis_server.Hubs;

public class GameHub : Hub
{
    private static readonly Dictionary<string, GameRoom> rooms = [];

    public async Task CreateRoom(string roomId)
    {
        if (!rooms.ContainsKey(roomId))
        {
            rooms[roomId] = new GameRoom(roomId);
            rooms[roomId].StartEnemyAttackLoop(this);
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
    }

    public async Task Attack(string roomId)
    {
        if (rooms.TryGetValue(roomId, out GameRoom? value))
        {
            value.Enemy.TakeDamage(1);
            await Clients.Group(roomId).SendAsync("EnemyAttacked", value.Enemy.Health);
        }
    }

    public async Task Dodge(string roomId)
    {
        if (rooms.TryGetValue(roomId, out GameRoom? value))
        {
            await value.Players[Context.ConnectionId].Dodge();
            await Clients.Client(Context.ConnectionId).SendAsync("Dodged");
        }
    }
}

public class GameRoom(string roomId)
{
    public string RoomId { get; } = roomId;
    public Dictionary<string, Player> Players { get; } = new();
    public Enemy Enemy { get; } = new();

    public void StartEnemyAttackLoop(GameHub hub)
    {
        Task.Run(async () =>
        {
            while (Enemy.Health > 0)
            {
                await Task.Delay(3000);
                foreach (var player in Players.Values)
                {
                    if (!player.IsInvulnerable)
                    {
                        player.Health -= 10;
                        await hub.Clients.Client(player.ConnectionId).SendAsync("TakeDamage", player.Health);
                    }
                }
            }
        });
    }
}

public class Player
{
    public string ConnectionId { get; }
    public int Health { get; set; } = 100;
    public bool IsInvulnerable { get; private set; }

    public async Task Dodge()
    {
        IsInvulnerable = true;
        await Task.Delay(500).ContinueWith(_ => IsInvulnerable = false);
    }
}

public class Enemy
{
    public int Health { get; set; } = 100;

    public int TakeDamage(int damage)
    {
        Health -= damage;
        return Health;
    }
}