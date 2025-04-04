using Domain.Entities;
using Infrastructure.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Config;

public class VordrinDbContext : DbContext
{
    public VordrinDbContext(DbContextOptions<VordrinDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    public DbSet<UserDto> Users { get; set; }
    public DbSet<CharacterDto> Characters { get; set; }
    public DbSet<WeaponDto> Weapons { get; set; }
}