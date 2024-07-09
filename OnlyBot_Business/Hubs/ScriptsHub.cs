using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OnlyBot_Business.IRepository;
using OnlyBot_DataAccess;

namespace OnlyBot_Business.Hubs
{
    public class ScriptsHub : Hub
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ScriptsHub(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task RefreshScripts()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var scripts = dbContext.Scripts.ToList();
                var settings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                if (Clients != null)
                {
                    await Clients.All.SendAsync("RefreshScripts", JsonConvert.SerializeObject(scripts, Formatting.Indented, settings));
                }
            }
        }
    }
}