using Microsoft.JSInterop;
using OnlyBot_Client.Shared.OnlyBotComponents.Scripts;

namespace OnlyBot_Client.Helpers
{
    public static class SearchHelper
    {
        [JSInvokable("UpdateSearchQuery")]
        public static void UpdateSearchQuery(string query)
        {
            //_ScriptSearchBar.Instance.UpdateSearchQuery(query);
        }
    }
}
