using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OnlyBot_Business.Hubs;
using OnlyBot_Business.IRepository;
using OnlyBot_DataAccess;
using OnlyBot_Models;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace OnlyBot_Business
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IHubContext<InventoriesHub> _hubContext;
        private readonly ApplicationDbContext _context;
        private readonly SqlTableDependency<Inventory> _dependency;
        public InventoryRepository(ApplicationDbContext context, IHubContext<InventoriesHub> hubContext, IConfiguration configuration)
        {
            _context = context;
            _hubContext = hubContext;
            _dependency = new SqlTableDependency<Inventory>(configuration.GetConnectionString("DefaultConnection"), "Inventories");
            _dependency.OnChanged += Changed;
            _dependency.Start();
        }

        private async void Changed(object sender, RecordChangedEventArgs<Inventory> e)
        {
            var inventories = await GetAll();
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            await _hubContext.Clients.All.SendAsync("RefreshInventories", JsonConvert.SerializeObject(inventories, Formatting.Indented, settings));
        }

        public async Task<Inventory> Create(Inventory Inventory)
        {
            var inventory = Inventory;

            var addedInventory = await _context.Inventories.AddAsync(inventory);
            await _context.SaveChangesAsync();

            return addedInventory.Entity;
        }

        public async Task<int> Delete(Guid id)
        {
            var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.Id == id);

            if (inventory != null)
            {
                _context.Inventories.Remove(inventory);
                return await _context.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<Inventory> Get(Guid id)
        {
            var inventory = await _context.Inventories
                .AsNoTracking()
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inventory != null)
            {
                return inventory;
            }

            return new Inventory();
        }

        public async Task<IEnumerable<Inventory>> GetAll()
        {
            return await _context.Inventories
                .AsNoTracking()
                .Include(i => i.Items)
                .ToListAsync();
        }

        public async Task<Inventory> Update(Inventory Inventory)
        {
            var inventoryFromDb = await _context.Inventories.FirstOrDefaultAsync(i => i.Id == Inventory.Id);

            if (inventoryFromDb != null)
            {
                inventoryFromDb.Id = Inventory.Id;
                inventoryFromDb.Bot = Inventory.Bot;
                Inventory.Kamas = Inventory.Kamas;
                Inventory.Pods = Inventory.Pods;
                Inventory.MaxPods = Inventory.MaxPods;
                Inventory.SlotsMax = Inventory.SlotsMax;
                Inventory.SlotsUsed = Inventory.SlotsUsed;
                Inventory.Items = Inventory.Items;
                Inventory.Type = Inventory.Type;

                await _context.SaveChangesAsync();

                return inventoryFromDb;
            }

            return Inventory;
        }
    }
}
