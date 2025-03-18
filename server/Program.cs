using System.Collections.Concurrent;
using SocketIOSharp.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .WithOrigins("http://localhost:3000")
              .AllowCredentials();
    });
});

var app = builder.Build();
app.UseCors();

// Store connected clients
var connectedClients = new ConcurrentDictionary<string, SocketIOSocket>();

// Game state
var gameState = new
{
    players = new List<object>(),
    enemies = new List<object>
    {
        new
        {
            id = "enemy1",
            name = "Dragon",
            health = 100,
            healthMax = 100
        }
    }
};

// Socket.IO server setup
var socketServer = new SocketIOServer(new SocketIOServerOption(5055));

socketServer.OnConnection((socket) =>
{
    var socketId = socket.Id;
    connectedClients.TryAdd(socketId, socket);
    Console.WriteLine($"Client connected: {socketId}");

    // Send initial game state
    socket.Emit("gameStateUpdate", gameState);

    socket.On("playerAttack", (data) =>
    {
        Console.WriteLine($"Player attack received: {data}");
        socket.Emit("enemyDamaged", new
        {
            enemyId = "enemy1",
            newHealth = 90 // Example: reduce health by 10
        });
    });

    socket.OnDisconnect(() =>
    {
        Console.WriteLine($"Client disconnected: {socketId}");
        connectedClients.TryRemove(socketId, out _);
    });
});

socketServer.Start();

Console.WriteLine("Socket.IO server running on port 5055");

app.Run();