using OnlyBot_Client.IServices;
using OnlyBot_Models;
using System.Net.Http.Json;

namespace OnlyBot_Client.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Create(Order order)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Order", order);
            
            response.EnsureSuccessStatusCode();
        }

        public async Task<int> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Order/{id}");

            if (response.IsSuccessStatusCode)
            {
                return 1;
            }

            return 0;
        }

        public async Task<Order> Get(Guid id)
        {
            var order = await _httpClient.GetFromJsonAsync<Order>($"api/Order/{id}");

            if (order != null)
            {
                return order;
            }

            return new Order();
        }

        public async Task<List<Order>> GetAll()
        {
            var orders = await _httpClient.GetFromJsonAsync<List<Order>>("api/Order");

            if (orders != null)
            {
                return orders;
            }

            return new List<Order>();
        }

        public async Task<List<Order>> GetFilteredOrders(Dictionary<string, List<string>> filters)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Order order)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Order", order);

            response.EnsureSuccessStatusCode();
        }
    }
}
