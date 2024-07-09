using OnlyBot_Client.IServices;
using OnlyBot_Models;
using System.Net.Http.Json;

namespace OnlyBot_Client.Services
{
    public class ScriptService : IScriptService
    {
        private readonly HttpClient _httpClient;

        public ScriptService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Create(Script script)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Script", script);

            response.EnsureSuccessStatusCode();
        }

        public async Task<int> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Script/{id}");

            if (response.IsSuccessStatusCode)
            {
                return 1;
            }

            return 0;
        }

        public async Task<Script> Get(Guid id)
        {
            var script = await _httpClient.GetFromJsonAsync<Script>($"api/Script/{id}");

            if (script != null)
            {
                return script;
            }

            return new Script();
        }

        public async Task<List<Script>> GetAll()
        {
            var scripts = await _httpClient.GetFromJsonAsync<List<Script>>("api/Script");

            if (scripts != null)
            {
                return scripts;
            }

            return new List<Script>();
        }

        public async Task<List<Script>> GetFilteredScripts(Dictionary<string, List<string>> filters)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Script/filteredscripts", filters);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<Script>>();

                if (result == null)
                {
                    return new List<Script>();
                }

                return result;
            }
            else
            {
                throw new HttpRequestException($"Erreur dans l'appel API: {response.ReasonPhrase}");

            }
        }

        public async Task Update(Script script)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Script", script);

            response.EnsureSuccessStatusCode();
        }
    }
}
