using OnlyBot_Models;

namespace OnlyBot.IServices
{
    public interface IInventoryService
    {
        public Task<Inventory> Create(Inventory inventory);
        public Task<Inventory> Update(Inventory inventory);
        public Task<int> Delete(Guid id);
        public Task<Inventory> Get(Guid id);
        public Task<List<Inventory>> GetAll();
        public Task<List<Inventory>> GetFilteredInventories(Dictionary<string, List<string>> filters);
    }
}