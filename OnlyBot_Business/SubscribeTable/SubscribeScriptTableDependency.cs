using OnlyBot_Business.Hubs;
using OnlyBot_Models;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace OnlyBot_Business.SubscribeTable
{
    public class SubscribeScriptTableDependency : ISubscribeTableDependency
    {
        private SqlTableDependency<Script> _dependency;
        private ScriptsHub _scriptsHub;

        public SubscribeScriptTableDependency(ScriptsHub scriptsHub)
        {
            _scriptsHub = scriptsHub;
        }

        public void SubscribeTableDependency(string connectionString)
        {
            _dependency = new SqlTableDependency<Script>(connectionString, "Scripts");
            _dependency.OnChanged += TableDependency_OnChanged;
            _dependency.Start();
        }

        private async void TableDependency_OnChanged(object sender, RecordChangedEventArgs<Script> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                await _scriptsHub.RefreshScripts();
            }
        }
    }
}
