using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace YarpSignalRSample.SignalR.Hubs;

[Authorize]
public class NotificationHub : Hub
{
    public async Task Join(string message)
    {
        await Clients.All.SendAsync("Send", message);
    }
}