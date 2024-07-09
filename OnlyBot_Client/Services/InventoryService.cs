using OnlyBot_Client.IServices;
using OnlyBot_Models;
using System.Net.Http.Json;

namespace OnlyBot_Client.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly HttpClient _httpClient;
        public InventoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Inventory/{id}");

            if (response.IsSuccessStatusCode)
            {
                return 1;
            }

            return 0;
        }

        public async Task<Inventory> Get(Guid id)
        {
            var inventory = await _httpClient.GetFromJsonAsync<Inventory>($"api/Inventory/{id}");

            if (inventory != null)
            {
                return inventory;
            }

            return new Inventory();
        }

        public async Task<List<Inventory>> GetAll()
        {
            var inventories = await _httpClient.GetFromJsonAsync<List<Inventory>>("api/Inventory");

            if (inventories != null)
            {
                return inventories;
            }

            return new List<Inventory>();
        }

        public async Task<List<Inventory>> GetFilteredInventories(Dictionary<string, List<string>> filters)
        {
            throw new NotImplementedException();
        }
        public async Task Create(Inventory inventory)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Inventory", inventory);

            response.EnsureSuccessStatusCode();
        }
        public async Task Update(Inventory inventory)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Inventory", inventory);

            response.EnsureSuccessStatusCode();
        }
    }
}
