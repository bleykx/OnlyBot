using OnlyBot_Models;

namespace OnlyBot_Business.IRepository
{
    public interface IItemRepository
    {
        public Task<Item> Create(Item Item);
        public Task<Item> Update(Item Item);
        public Task<int> Delete(Guid id);
        public Task<Item> Get(Guid id);
        public Task<IEnumerable<Item>> GetAll();
    }
}