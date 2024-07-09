using OnlyBot_Models;

namespace OnlyBot.IServices
{
    public interface IOrderService
    {
        public Task<Order> Create(Order order);
        public Task<Order> Update(Order order);
        public Task<int> Delete(Guid id);
        public Task<Order> Get(Guid id);
        public Task<List<Order>> GetAll();
        public Task<List<Order>> GetFilteredOrders(Dictionary<string, List<string>> filters);
    }
}
