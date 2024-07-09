using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OnlyBot_Business.IRepository;
using OnlyBot_DataAccess;
using OnlyBot_Models;

namespace OnlyBot_Business.Hubs
{
    public class ProxiesHub : Hub
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ProxiesHub(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;

        }

        public async Task RefreshProxies()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var proxies = dbContext.Proxies.ToList();
                var settings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                if (Clients != null)
                {
                    await Clients.All.SendAsync("RefreshProxies", JsonConvert.SerializeObject(proxies, Formatting.Indented, settings));
                }
            }
        }
    }
}