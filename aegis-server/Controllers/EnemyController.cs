using aegis_server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aegis_server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnemyController : ControllerBase
{
    private readonly AegisDbContext _context;

    public EnemyController(AegisDbContext context)
    {
        _context = context;
    }

    [HttpPost("attack")]
    public async Task<IActionResult> AttackEnemy(Guid sessionId, int damage)
    {
        var enemy = await _context.Enemies.FirstOrDefaultAsync(e => e.SessionId == sessionId);
        if (enemy == null) return NotFound("Enemy not found");

        enemy.Hp = Math.Max(0, enemy.Hp - damage);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Enemy attacked!", newHp = enemy.Hp });
    }
}
