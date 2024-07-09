using OnlyBot_Client.IServices;
using OnlyBot_Models;
using System.Net.Http.Json;

namespace OnlyBot_Client.Services
{
    public class ProxyService : IProxyService
    {
        private readonly HttpClient _httpClient;

        public ProxyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Create(Proxy proxy)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Proxy", proxy);

            response.EnsureSuccessStatusCode();
        }

        public async Task<int> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Proxy/{id}");

            if (response.IsSuccessStatusCode)
            {
                return 1;
            }

            return 0;
        }

        public async Task<Proxy> Get(Guid id)
        {
            var proxy = await _httpClient.GetFromJsonAsync<Proxy>($"api/Proxy/{id}");

            if (proxy != null)
            {
                return proxy;
            }

            return new Proxy();
        }

        public async Task<List<Proxy>> GetAll()
        {
            var proxies = await _httpClient.GetFromJsonAsync<List<Proxy>>("api/Proxy");

            if (proxies != null)
            {
                return proxies;
            }

            return new List<Proxy>();
        }

        public async Task<List<Proxy>> GetFilteredProxies(Dictionary<string, List<string>> filters)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Proxy proxy)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Proxy", proxy);

            response.EnsureSuccessStatusCode();
        }
    }
}
