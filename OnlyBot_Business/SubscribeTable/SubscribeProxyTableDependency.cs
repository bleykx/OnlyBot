using OnlyBot_Business.Hubs;
using OnlyBot_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace OnlyBot_Business.SubscribeTable
{
    public class SubscribeProxyTableDependency : ISubscribeTableDependency
    {
        private SqlTableDependency<Proxy> _dependency;
        private ProxiesHub _proxiesHub;

        public SubscribeProxyTableDependency(ProxiesHub proxiesHub)
        {
            _proxiesHub = proxiesHub;
        }

        public void SubscribeTableDependency(string connectionString)
        {
            _dependency = new SqlTableDependency<Proxy>(connectionString, "Proxies");
            _dependency.OnChanged += TableDependency_OnChanged;
            _dependency.Start();
        }

        private async void TableDependency_OnChanged(object sender, RecordChangedEventArgs<Proxy> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                await _proxiesHub.RefreshProxies();
            }
        }
    }
}
