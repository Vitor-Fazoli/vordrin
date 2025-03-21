using aegis_server.Models;
using Microsoft.EntityFrameworkCore;

namespace aegis_server.Data;

public class AegisDbContext(DbContextOptions<AegisDbContext> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=app.db");
    }

    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Enemy> Enemies { get; set; }
    public DbSet<Weapon> Weapons { get; set; }
    public DbSet<WeaponType> WeaponTypes { get; set; }
}