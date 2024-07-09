using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OnlyBot_Business.IRepository;
using OnlyBot_DataAccess;
using OnlyBot_Models;

namespace OnlyBot_Business.Hubs
{
    public class OrdersHub : Hub
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public OrdersHub(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task RefreshOrders()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var orders = dbContext.Orders.ToList();

                var settings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                if (Clients != null)
                {
                    await Clients.All.SendAsync("RefreshOrders", JsonConvert.SerializeObject(orders, Formatting.Indented, settings));
                }
            }
        }
    }
}