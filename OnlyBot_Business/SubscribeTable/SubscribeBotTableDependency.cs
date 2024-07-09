using Microsoft.Extensions.DependencyInjection;
using OnlyBot_Business.Hubs;
using OnlyBot_Models;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace OnlyBot_Business.SubscribeTable
{
    public class SubscribeBotTableDependency : ISubscribeTableDependency
    {
        private SqlTableDependency<Bot> _dependency;
        private BotsHub _botHub;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SubscribeBotTableDependency(BotsHub botHub)
        {
            _botHub = botHub;
        }
        public void SubscribeTableDependency(string connectionString)
        {
            _dependency = new SqlTableDependency<Bot>(connectionString, "Bots");
            _dependency.OnChanged += TableDependency_OnChanged;
            _dependency.Start();
        }

        private async void TableDependency_OnChanged(object sender, RecordChangedEventArgs<Bot> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                await _botHub.RefreshBots();
            }
        }
    }
}
