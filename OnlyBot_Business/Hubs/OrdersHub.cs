using Microsoft.AspNetCore.SignalR;

namespace OnlyBot_Business.Hubs
{
    public class OrdersHub : Hub
    {
        public async Task RefreshOrders(string orders)
        {
            await Clients.All.SendAsync("RefreshOrders", orders);
        }
    }
}