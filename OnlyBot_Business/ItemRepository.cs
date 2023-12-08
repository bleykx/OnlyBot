using Microsoft.EntityFrameworkCore;
using OnlyBot_Business.IRepository;
using OnlyBot_DataAccess;
using OnlyBot_Models;

namespace OnlyBot_Business
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

    public async Task<Item> Create(Item Item)
        {
            var item = Item;
            
            var addedItem = await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();

            return addedItem.Entity;
        }

        public async Task<int> Delete(Guid id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == id);

            if(item != null)
            {
                _context.Items.Remove(item);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<Item> Get(Guid id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == id);

            if (item != null)
            {
                return item;
            }

            return new Item();
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            return await _context.Items.ToListAsync();  
        }

        public async Task<Item> Update(Item Item)
        {
            var itemFromDb = await _context.Items.FirstOrDefaultAsync(i => i.Id == Item.Id);

            if(itemFromDb != null)
            {
                itemFromDb.Id = Item.Id;
                itemFromDb.ItemId = Item.ItemId;
                itemFromDb.ImgLink = Item.ImgLink;
                itemFromDb.Name = Item.Name;
                itemFromDb.Level = Item.Level;
                itemFromDb.Quantity = Item.Quantity;
                itemFromDb.Weight = Item.Weight;
                itemFromDb.Type = Item.Type;
                itemFromDb.SubType = Item.SubType;

                await _context.SaveChangesAsync();

                return itemFromDb;
            }

            return Item;
        }
    }
}
