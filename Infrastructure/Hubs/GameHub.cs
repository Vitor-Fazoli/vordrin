
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace Infrastructure.Hubs;


// Classe para representar um jogador
public class Player
{
    public string ConnectionId { get; set; }
    public string Username { get; set; }
    public int Health { get; set; } = 100;
    public int Damage { get; set; } = 10;
    public bool IsDefending { get; set; } = false;
    public bool IsEvading { get; set; } = false;
}

// Classe para representar um inimigo
public class Enemy
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Damage { get; set; }
    public int AttackCooldown { get; set; } // Em milissegundos
    public DateTime LastAttackTime { get; set; }
    public bool IsAttacking { get; set; }
}

public class GameHub : Hub
{
    private static ConcurrentDictionary<string, Player> _connectedPlayers = new ConcurrentDictionary<string, Player>();
    private static List<Enemy> _enemies = [];
    private static Random _random = new();
    private static int _enemyIdCounter = 1;

    public override async Task OnConnectedAsync()
    {
        var playerId = Context.ConnectionId;
        var player = new Player { ConnectionId = playerId, Username = $"Player_{playerId[..5]}" };
        _connectedPlayers.TryAdd(playerId, player);

        // Se não houver inimigos, cria um
        if (_enemies.Count == 0)
        {
            SpawnEnemy();
        }

        await Clients.Caller.SendAsync("PlayerJoined", player);
        await Clients.Caller.SendAsync("UpdateEnemies", _enemies);
        await Clients.Others.SendAsync("PlayerConnected", player);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var playerId = Context.ConnectionId;
        _connectedPlayers.TryRemove(playerId, out var player);
        await Clients.Others.SendAsync("PlayerDisconnected", playerId);
        await base.OnDisconnectedAsync(exception);
    }

    public async Task PrepareEnemyAttack(int enemyId, string targetPlayerId)
    {
        var enemy = _enemies.Find(e => e.Id == enemyId);
        if (enemy == null)
            return;

        if (!_connectedPlayers.TryGetValue(targetPlayerId, out var player))
            return;

        // Notificar que o ataque está próximo
        await Clients.All.SendAsync("EnemyPreparingAttack", enemyId, targetPlayerId);

        // Esperar 1.5 segundos antes de realizar o ataque
        await Task.Delay(1500);

        // Realizar o ataque
        await ProcessEnemyAttack(enemyId, targetPlayerId);
    }
    public async Task AttackEnemy(int enemyId)
    {
        var playerId = Context.ConnectionId;
        if (!_connectedPlayers.TryGetValue(playerId, out var player))
            return;

        var enemy = _enemies.Find(e => e.Id == enemyId);
        if (enemy == null)
            return;

        // Causar dano ao inimigo
        enemy.Health -= player.Damage;

        // Verificar se o inimigo foi derrotado
        if (enemy.Health <= 0)
        {
            _enemies.Remove(enemy);
            await Clients.All.SendAsync("EnemyDefeated", enemyId);

            // Criar novo inimigo
            SpawnEnemy();
        }
        else
        {
            // Atualizar o estado do inimigo para todos os jogadores
            await Clients.All.SendAsync("UpdateEnemy", enemy);
        }
    }

    // Método para defender (clique direito durante um ataque do inimigo)
    public async Task Defend()
    {
        var playerId = Context.ConnectionId;
        if (!_connectedPlayers.TryGetValue(playerId, out var player))
            return;

        // Ativar modo de defesa
        player.IsDefending = true;

        // Enviar atualização para todos
        await Clients.All.SendAsync("PlayerDefending", playerId);

        // Desativar defesa após um curto período
        await Task.Delay(500);
        player.IsDefending = false;

        await Clients.All.SendAsync("PlayerStoppedDefending", playerId);
    }

    // Método para esquivar (pressionar shift durante um ataque)
    public async Task Evade()
    {
        var playerId = Context.ConnectionId;
        if (!_connectedPlayers.TryGetValue(playerId, out var player))
            return;

        // Ativar modo de esquiva
        player.IsEvading = true;

        // Enviar atualização para todos
        await Clients.All.SendAsync("PlayerEvading", playerId);

        // Desativar esquiva após um curto período
        await Task.Delay(300);
        player.IsEvading = false;

        await Clients.All.SendAsync("PlayerStoppedEvading", playerId);
    }

    // Método para processar um ataque do inimigo
    public async Task ProcessEnemyAttack(int enemyId, string targetPlayerId)
    {
        var enemy = _enemies.Find(e => e.Id == enemyId);
        if (enemy == null)
            return;

        if (!_connectedPlayers.TryGetValue(targetPlayerId, out var player))
            return;

        // Iniciar ataque do inimigo
        enemy.IsAttacking = true;
        enemy.LastAttackTime = DateTime.Now;

        // Notificar todos os jogadores que o inimigo está atacando
        await Clients.All.SendAsync("EnemyAttacking", enemyId, targetPlayerId);

        // Dar tempo para o jogador reagir
        await Task.Delay(1000);

        // Verificar se o jogador defendeu ou esquivou
        if (player.IsDefending)
        {
            // Dano reduzido pela metade se estiver defendendo
            player.Health -= enemy.Damage / 2;
            await Clients.All.SendAsync("PlayerTookReducedDamage", targetPlayerId, enemy.Damage / 2);
        }
        else if (player.IsEvading)
        {
            // Sem dano se esquivou com sucesso
            await Clients.All.SendAsync("PlayerEvadedAttack", targetPlayerId);
        }
        else
        {
            // Dano total
            player.Health -= enemy.Damage;
            await Clients.All.SendAsync("PlayerTookDamage", targetPlayerId, enemy.Damage);

            // Verificar se o jogador morreu
            if (player.Health <= 0)
            {
                player.Health = 100; // Reviver com saúde cheia
                await Clients.All.SendAsync("PlayerDied", targetPlayerId);
                await Clients.Client(targetPlayerId).SendAsync("YouDied");
            }
        }

        // Finalizar ataque do inimigo
        enemy.IsAttacking = false;
        await Clients.All.SendAsync("EnemyStoppedAttacking", enemyId);

        // Atualizar estado do jogador
        await Clients.All.SendAsync("UpdatePlayer", player);
    }

    // Método para criar um novo inimigo
    private void SpawnEnemy()
    {
        var enemy = new Enemy
        {
            Id = _enemyIdCounter++,
            Name = GetRandomEnemyName(),
            Health = _random.Next(50, 200 * _connectedPlayers.Count),
            Damage = _random.Next(5, 20),
            AttackCooldown = _random.Next(3000, 8000),
            LastAttackTime = DateTime.Now.AddSeconds(-10),
            IsAttacking = false
        };

        enemy.MaxHealth = enemy.Health;
        _enemies.Add(enemy);

        // Notificar todos os jogadores sobre o novo inimigo
        Clients.All.SendAsync("NewEnemy", enemy);
    }

    // Método auxiliar para gerar nomes de inimigos aleatórios
    private string GetRandomEnemyName()
    {
        string[] prefixes = { "Terrível", "Sombrio", "Furioso", "Brutal", "Cruel" };
        string[] types = { "Ogro", "Dragão", "Esqueleto", "Zumbi", "Demônio", "Troll" };

        return $"{prefixes[_random.Next(prefixes.Length)]} {types[_random.Next(types.Length)]}";
    }

    // Método para inimigos atacarem jogadores eventualmente
    private async Task EnemyAttackLoop()
    {
        while (true)
        {
            await Task.Delay(1000); // Esperar 1 segundo entre verificações

            foreach (var enemy in _enemies)
            {
                if (enemy.IsAttacking || (DateTime.Now - enemy.LastAttackTime).TotalMilliseconds < enemy.AttackCooldown)
                    continue;

                var targetPlayer = _connectedPlayers.Values.OrderBy(_ => _random.Next()).FirstOrDefault();
                if (targetPlayer != null)
                {
                    _ = ProcessEnemyAttack(enemy.Id, targetPlayer.ConnectionId);
                }
            }
        }
    }

    private static bool _enemyLoopStarted = false;

    public void StartEnemyAttackLoop()
    {
        if (_enemyLoopStarted) return;

        _enemyLoopStarted = true;
        _ = Task.Run(async () =>
        {
            while (true)
            {
                await Task.Delay(1000);

                foreach (var enemy in _enemies)
                {
                    if (enemy.IsAttacking || (DateTime.Now - enemy.LastAttackTime).TotalMilliseconds < enemy.AttackCooldown)
                        continue;

                    var targetPlayer = _connectedPlayers.Values.OrderBy(_ => _random.Next()).FirstOrDefault();
                    if (targetPlayer != null)
                    {
                        await PrepareEnemyAttack(enemy.Id, targetPlayer.ConnectionId);
                    }
                }
            }
        });
    }


    // Construtor estático para iniciar o loop de ataque
    static GameHub()
    {
        _ = Task.Run(() => new GameHub().EnemyAttackLoop());
    }
}
