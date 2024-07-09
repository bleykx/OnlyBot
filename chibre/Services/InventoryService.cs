using OnlyBot_Client.IServices;
using OnlyBot_Models;

namespace OnlyBot_Client.Services
{
    public class InventoryService : IInventoryService
    {
        public Task<Inventory> Create(Inventory inventory)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Inventory> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Inventory>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Inventory>> GetFilteredInventories(Dictionary<string, List<string>> filters)
        {
            throw new NotImplementedException();
        }

        public Task<Inventory> Update(Inventory inventory)
        {
            throw new NotImplementedException();
        }
    }
}
