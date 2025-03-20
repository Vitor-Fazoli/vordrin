using Microsoft.AspNetCore.Mvc;

namespace aegis_server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttackController : ControllerBase
{
    private static int enemyHp = 100;

    // Endpoint para processar o ataque
    [HttpPost]
    public ActionResult Attack([FromBody] AttackRequest request)
    {
        // Processar dano
        enemyHp -= request.Damage;

        if (enemyHp < 0) enemyHp = 0;

        // Retornar a vida atualizada do inimigo
        return Ok(new { enemyHp });
    }
}

public class AttackRequest
{
    public string? SessionId { get; set; }
    public int Damage { get; set; }
}
