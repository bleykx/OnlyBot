using Microsoft.AspNetCore.SignalR;

namespace OnlyBot_Business.Hubs
{
    public class BotsHub : Hub
    {
        public async Task RefreshBots(string bots)
        {
            await Clients.All.SendAsync("RefreshBots", bots);
        }
    }
}