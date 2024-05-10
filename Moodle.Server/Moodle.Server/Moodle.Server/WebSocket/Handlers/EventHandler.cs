using System.Net.WebSockets;
using System.Text;

namespace Moodle.Server.WebSocket.Handlers
{
    public class EventsHandler : WebSocketHandler
    {
        public EventsHandler(ConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {
        }
        public override async Task ReceiveAsync(System.Net.WebSockets.WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var message = $"New event has been created: {Encoding.UTF8.GetString(buffer, 0, result.Count)}";

            await SendMessageToAllAsync(message);
        }
    }
}
