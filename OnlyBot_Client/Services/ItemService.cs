using OnlyBot_Client.IServices;
using OnlyBot_Models;
using System.Net.Http.Json;

namespace OnlyBot_Client.Services
{
    public class ItemService : IItemService
    {
        private readonly HttpClient _httpClient;
        
        public ItemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Create(Item item)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Item", item);

            response.EnsureSuccessStatusCode();
        }

        public async Task<int> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Item/{id}");

            if (response.IsSuccessStatusCode)
            {
                return 1;
            }

            return 0;
        }

        public async Task<Item> Get(Guid id)
        {
            var item = await _httpClient.GetFromJsonAsync<Item>($"api/Item/{id}");

            if (item != null)
            {
                return item;
            }

            return new Item();
        }

        public async Task<List<Item>> GetAll()
        {
            var items = await _httpClient.GetFromJsonAsync<List<Item>>("api/Item");

            if (items != null)
            {
                return items;
            }

            return new List<Item>();
        }

        public async Task<List<Item>> GetFilteredItems(Dictionary<string, List<string>> filters)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Item item)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Item", item);

            response.EnsureSuccessStatusCode();
        }
    }
}
