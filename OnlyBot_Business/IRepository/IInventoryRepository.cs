using OnlyBot_Models;

namespace OnlyBot_Business.IRepository
{
    public interface IInventoryRepository
    {
        public Task<Inventory> Create(Inventory Inventory);
        public Task<Inventory> Update(Inventory Inventory);
        public Task<int> Delete(Guid id);
        public Task<Inventory> Get(Guid id);
        public Task<IEnumerable<Inventory>> GetAll();
    }
}
