using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWebSockets();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/ws")
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            await HandleConnection(webSocket);
        }
        else
        {
            context.Response.StatusCode = 400;
        }
    }
    else
    {
        await next();
    }
});

static async Task HandleConnection(WebSocket socket)
{
    var buffer = new byte[1024 * 4];

    while (socket.State == WebSocketState.Open)
    {
        var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        if (result.MessageType == WebSocketMessageType.Close)
        {
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Conexão fechada", CancellationToken.None);
            break;
        }
        else
        {
            var receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
            Console.WriteLine($"Mensagem recebida: {receivedMessage}");

            var responseMessage = $"Servidor recebeu: {receivedMessage}";
            var responseBytes = Encoding.UTF8.GetBytes(responseMessage);
            await socket.SendAsync(new ArraySegment<byte>(responseBytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}

app.Run();