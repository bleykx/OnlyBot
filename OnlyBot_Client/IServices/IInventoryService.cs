using OnlyBot_Models;

namespace OnlyBot_Client.IServices
{
    public interface IInventoryService
    {
        public Task Create(Inventory inventory);
        public Task Update(Inventory inventory);
        public Task<int> Delete(Guid id);
        public Task<Inventory> Get(Guid id);
        public Task<List<Inventory>> GetAll();
        public Task<List<Inventory>> GetFilteredInventories(Dictionary<string, List<string>> filters);
    }
}