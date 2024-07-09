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
    public class SubscribeInventoryTableDependency : ISubscribeTableDependency
    {
        private SqlTableDependency<Inventory> _dependency;
        private InventoriesHub _inventoriesHub;

        public SubscribeInventoryTableDependency(InventoriesHub inventoriesHub)
        {
            _inventoriesHub = inventoriesHub;
        }

        public void SubscribeTableDependency(string connectionString)
        {
            _dependency = new SqlTableDependency<Inventory>(connectionString, "Inventories");
            _dependency.OnChanged += TableDependency_OnChanged;
            _dependency.Start();
        }

        private async void TableDependency_OnChanged(object sender, RecordChangedEventArgs<Inventory> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                await _inventoriesHub.RefreshInventories();
            }
        }
    }
}
