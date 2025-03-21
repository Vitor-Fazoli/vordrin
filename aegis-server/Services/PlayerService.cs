using aegis_server.Data;
using aegis_server.Models;
using Microsoft.EntityFrameworkCore;

namespace aegis_server.Services
{
    public class PlayerService(AegisDbContext dbContext)
    {
        private readonly AegisDbContext _dbContext = dbContext;

        public async Task<Player> CreatePlayer(string id, string name)
        {
            var player = new Player(id, name);
            _dbContext.Players.Add(player);
            await _dbContext.SaveChangesAsync();
            return player;
        }

        public async Task<Player?> GetPlayer(Guid id)
        {
            return await _dbContext.Players.FindAsync(id);
        }
    }
}