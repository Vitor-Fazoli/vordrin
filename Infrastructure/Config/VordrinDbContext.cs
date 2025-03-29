using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Config;

public class VordrinDbContext : DbContext
{
    public VordrinDbContext(DbContextOptions<VordrinDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Character> Characters { get; set; }
}