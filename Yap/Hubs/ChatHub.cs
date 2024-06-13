using Microsoft.AspNetCore.SignalR;

namespace Yap.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string chatroomName, string user, string message)
        {
            await Clients.Group(chatroomName).SendAsync("ReceiveMessage", user, message);
        }

        public Task JoinChatroom(string chatroomName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, chatroomName);
        }

        public Task LeaveChatroom(string chatroomName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, chatroomName);
        }
    }
}