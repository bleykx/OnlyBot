using OnlyBot_Client.IServices;
using OnlyBot_Models;

namespace OnlyBot_Client.Services
{
    public class ItemService : IItemService
    {
        public Task<Item> Create(Item item)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Item> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Item>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Item>> GetFilteredItems(Dictionary<string, List<string>> filters)
        {
            throw new NotImplementedException();
        }

        public Task<Item> Update(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
