using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using aegis_server.Data;

namespace aegis_server.Hubs;

public class ChatHub : Hub
{
    private readonly AppDbContext _db;

    public ChatHub(AppDbContext db)
    {
        _db = db;
    }

    public async Task SendMessage(string user, string message)
    {
        // var newMessage = new Message { User = user, Content = message };
        // _db.Messages.Add(newMessage);
        await _db.SaveChangesAsync();

        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
