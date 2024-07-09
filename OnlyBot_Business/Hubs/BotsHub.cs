using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OnlyBot_Business.IRepository;
using OnlyBot_DataAccess;

namespace OnlyBot_Business.Hubs
{
    public class BotsHub : Hub
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BotsHub(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task RefreshBots()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var bots = dbContext.Bots
                .Include(b => b.Script)
                .Include(b => b.Inventories)
                .ThenInclude(i => i.Items)
                .Include(p => p.Proxy)
                .ToList();

                var settings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                if (Clients != null)
                {
                    await Clients.All.SendAsync("RefreshBots", JsonConvert.SerializeObject(bots, Formatting.Indented, settings));
                }
            }

        }
    }
}