using OnlyBot_Models;

namespace OnlyBot_Business.IRepository
{
    public interface IOrderRepository
    {
        public Task<Order> Create(Order order);
        public Task<Order> Update(Order order);
        public Task<int> Delete(Guid id);
        public Task<Order> Get(Guid id);
        public Task<List<Order>> GetAll();
    }
}
