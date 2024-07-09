using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OnlyBot_Business.IRepository;
using OnlyBot_DataAccess;
using OnlyBot_Models;

namespace OnlyBot_Business.Hubs
{
    public class InventoriesHub : Hub
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public InventoriesHub(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task RefreshInventories()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var inventories = dbContext.Inventories
                    .Include(i => i.Items)
                    .ToList();

                var settings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                if (Clients != null)
                {
                    await Clients.All.SendAsync("RefreshInventories", JsonConvert.SerializeObject(inventories, Formatting.Indented, settings));
                }
            }
        }
    }
}
