﻿using OnlyBot_Business.Hubs;
using OnlyBot_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableDependency.SqlClient;

namespace OnlyBot_Business.SubscribeTable
{
    public class SubscribeOrderTableDependency : ISubscribeTableDependency
    {
        private SqlTableDependency<Order> _dependency;
        private OrdersHub _ordersHub;

        public SubscribeOrderTableDependency(OrdersHub ordersHub)
        {
            _ordersHub = ordersHub;
        }

        public void SubscribeTableDependency(string connectionString)
        {
            _dependency = new SqlTableDependency<Order>(connectionString, "Orders");
            _dependency.OnChanged += TableDependency_OnChanged;
            _dependency.Start();
        }

        private async void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Order> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                await _ordersHub.RefreshOrders();
            }
        }
    }
}
