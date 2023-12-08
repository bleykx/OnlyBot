using Microsoft.AspNetCore.SignalR;

namespace OnlyBot_Business.Hubs
{
    public class ProxiesHub : Hub
    {
        public async Task RefreshProxies(string proxies)
        {
            await Clients.All.SendAsync("RefreshProxies", proxies);
        }
    }
}
