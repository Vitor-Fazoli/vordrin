using Microsoft.EntityFrameworkCore;
using aegis_server.Entities;

namespace aegis_server.Data;

public class AppDbContext : DbContext {

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=app.db");

    //public DbSet<Message>? Messages { get; set; }

    public DbSet<Player>? Players { get; set; }
    public DbSet<Item>? Items { get; set; }

}