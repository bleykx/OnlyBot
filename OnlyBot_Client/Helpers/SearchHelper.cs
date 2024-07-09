using Microsoft.JSInterop;
using OnlyBot_Client.Shared.OnlyBotComponents.Menus;

namespace OnlyBot_Client.Helpers
{
    public static class SearchHelper
    {
        [JSInvokable]
        public static void UpdateSearchQuery(string query)
        {
            _ScriptMenu.Instance.UpdateSearchQuery(query);
        }
    }
}
