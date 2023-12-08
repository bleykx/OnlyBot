using Microsoft.AspNetCore.SignalR;

namespace OnlyBot_Business.Hubs
{
    public class ScriptHub : Hub
    {
        public async Task RefreshScripts(string scripts)
        {
            await Clients.All.SendAsync("RefreshScripts", scripts);
        }
    }
}