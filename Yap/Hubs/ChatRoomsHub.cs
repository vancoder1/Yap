using Microsoft.AspNetCore.SignalR;

namespace Yap.Hubs
{
    public class ChatRoomsHub : Hub
    {
        public async Task SendChatroom(string chatroomName)
        {
            await Clients.All.SendAsync("ReceiveChatroom", chatroomName);
        }
    }
}
