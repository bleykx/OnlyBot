using OnlyBot_Models;
using TableDependency.SqlClient;
using OnlyBot_Business.Hubs;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace OnlyBot_Business.SubscribeTable
{
    public class SubscribeItemTableDependency : ISubscribeTableDependency
    {
        private SqlTableDependency<Item> _dependency;
        private ItemsHub _itemsHub;

        public SubscribeItemTableDependency(ItemsHub itemsHub)
        {
            _itemsHub = itemsHub;
        }

        public void SubscribeTableDependency(string connectionString)
        {
            _dependency = new SqlTableDependency<Item>(connectionString, "Items");
            _dependency.OnChanged += TableDependency_OnChanged;
            _dependency.Start();
        }

        private async void TableDependency_OnChanged(object sender, RecordChangedEventArgs<Item> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                await _itemsHub.RefreshItems();
            }
        }
    }
}
