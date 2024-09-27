using Microsoft.AspNetCore.SignalR;

namespace ChatSimple.Hub
{
    public class BroadcastHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("ReceiveMessage");
        }
    }
}
