using System;
using System.Collections.Generic;
using Domain.Entities;

public class GameRoom
{
    public string Code { get; set; }
    public List<Character> Players { get; set; } = [];
    public Enemy Enemy { get; private set; }
    public DateTime LastEnemyAttack { get; private set; } = DateTime.Now;

    public GameRoom(string code, Enemy enemy)
    {
        Code = code;
        Enemy = enemy;
    }

    public void AddPlayer(Character character)
    {
        Players.Add(character);
        Enemy.Aggro[character.GetId()] = new Domain.Entities.Attributes.Aggro();
    }

    public void PlayerAttack(Guid playerId)
    {
        var player = Players.Find(p => p.GetId() == playerId);
        if (player == null) return;

        player.Strike(Enemy);
    }

    public void EnemyStrike()
    {
        if ((DateTime.Now - LastEnemyAttack).TotalSeconds < 3) return;
        if (Players.Count == 0) return;

        var random = new Random();
        var target = Players[random.Next(Players.Count)];
        Enemy.Strike(target);

        LastEnemyAttack = DateTime.Now;
    }
}