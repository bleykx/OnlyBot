using Microsoft.AspNetCore.SignalR;

namespace OnlyBot_Business.Hubs
{
    public class InventoriesHub : Hub
    {
        public async Task RefreshInventories(string inventories)
        {
            await Clients.All.SendAsync("RefreshInventories", inventories);
        }
    }
}
