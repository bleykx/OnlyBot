using OnlyBot_Client.IServices;
using OnlyBot_Models;

namespace OnlyBot_Client.Services
{
    public class OrderService : IOrderService
    {
        public Task<Order> Create(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetFilteredOrders(Dictionary<string, List<string>> filters)
        {
            throw new NotImplementedException();
        }

        public Task<Order> Update(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
