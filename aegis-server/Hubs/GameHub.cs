using Microsoft.AspNetCore.SignalR;

namespace aegis_server.Hubs;

public class GameHub : Hub
{
    private static int enemyHp = 100;

    // Enviar a vida atualizada do inimigo para todos os jogadores
    public async Task SendEnemyHp()
    {
        await Clients.All.SendAsync("ReceiveEnemyHp", enemyHp);
    }

    // Função chamada quando um jogador faz um ataque
    public async Task Attack(string sessionId, int damage)
    {
        // Processar o dano
        enemyHp -= damage;

        if (enemyHp < 0) enemyHp = 0;

        // Enviar a vida atualizada do inimigo para todos os jogadores
        await SendEnemyHp();
    }
}
