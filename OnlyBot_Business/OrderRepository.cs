using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OnlyBot_Business.Hubs;
using OnlyBot_Business.IRepository;
using OnlyBot_DataAccess;
using OnlyBot_Models;
using TableDependency.SqlClient.Base.EventArgs;
using TableDependency.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace OnlyBot_Business
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IHubContext<OrdersHub> _hubContext;
        private readonly ApplicationDbContext _context;
        private readonly SqlTableDependency<Order> _dependency;
        public OrderRepository(ApplicationDbContext context, IHubContext<OrdersHub> hubContext, IConfiguration configuration)
        {
            _context = context;
            _hubContext = hubContext;
            _dependency = new SqlTableDependency<Order>(configuration.GetConnectionString("DefaultConnection"), "Order");
            _dependency.OnChanged += Changed;
            _dependency.Start();
        }

        private async void Changed(object sender, RecordChangedEventArgs<Order> e)
        {
            var orders = await GetAll();
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            await _hubContext.Clients.All.SendAsync("RefreshOrders", JsonConvert.SerializeObject(orders, Formatting.Indented, settings));
        }

        public async Task<Order> Create(Order order)
        {
            var addedOrder = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return addedOrder.Entity;
        }

        public async Task<int> Delete(Guid id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

            if (order != null)
            {
                _context.Orders.Remove(order);
                return await _context.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<Order> Get(Guid id)
        {
            var order = await _context.Orders
                .AsNoTracking()
                .Include(m => m.Bots)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order != null)
            {
                return order;
            }

            return new Order();
        }

        public async Task<List<Order>> GetAll()
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(m => m.Bots)
                .ToListAsync();
        }

        public async Task<Order> Update(Order order)
        {
            var orderFromDb = await _context.Orders.FirstOrDefaultAsync(o => o.Id == order.Id);

            if (orderFromDb != null)
            {
                orderFromDb.Id = order.Id;
                orderFromDb.OrderParams = order.OrderParams;
                orderFromDb.Description = order.Description;
                orderFromDb.Status = order.Status;
                orderFromDb.Bots = order.Bots;
                orderFromDb.CreatedAt = order.CreatedAt;
                orderFromDb.CompletedAt = order.CompletedAt;
                
                await _context.SaveChangesAsync();
                return orderFromDb;
            }
            
            return order;
        }
    }
}
