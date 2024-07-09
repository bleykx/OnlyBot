using OnlyBot_Models;

namespace OnlyBot_Client.IServices
{
    public interface IOrderService
    {
        public Task Create(Order order);
        public Task Update(Order order);
        public Task<int> Delete(Guid id);
        public Task<Order> Get(Guid id);
        public Task<List<Order>> GetAll();
        public Task<List<Order>> GetFilteredOrders(Dictionary<string, List<string>> filters);
    }
}
