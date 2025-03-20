using aegis_server.Models;
using Microsoft.EntityFrameworkCore;

namespace aegis_server.Data;

public class AegisDbContext(DbContextOptions<AegisDbContext> options) : DbContext(options)
{
    public DbSet<Player> Players { get; set; }
    public DbSet<Enemy> Enemies { get; set; }
}