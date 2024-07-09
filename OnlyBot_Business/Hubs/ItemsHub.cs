using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OnlyBot_Business.IRepository;
using OnlyBot_DataAccess;

namespace OnlyBot_Business.Hubs
{
    public class ItemsHub : Hub
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ItemsHub(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task RefreshItems()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var items = dbContext.Items.ToList();
                var settings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                if (Clients != null)
                {
                    await Clients.All.SendAsync("RefreshItems", JsonConvert.SerializeObject(items, Formatting.Indented, settings));
                }
            }
        }
    }
}
