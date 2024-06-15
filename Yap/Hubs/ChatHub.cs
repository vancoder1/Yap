using Microsoft.AspNetCore.SignalR;

namespace Yap.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string chatroomName, string user, string message)
        {
            await Clients.Group(chatroomName).SendAsync("ReceiveMessage", user, message);
        }
    }
}