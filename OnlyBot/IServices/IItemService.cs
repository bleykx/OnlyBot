using OnlyBot_Models;

namespace OnlyBot.IServices
{
    public interface IItemService
    {
        public Task<Item> Create(Item item);
        public Task<Item> Update(Item item);
        public Task<int> Delete(Guid id);
        public Task<Item> Get(Guid id);
        public Task<List<Item>> GetAll();
        public Task<List<Item>> GetFilteredItems(Dictionary<string, List<string>> filters);
    }
}
