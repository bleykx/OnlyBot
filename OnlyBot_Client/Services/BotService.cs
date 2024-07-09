using Microsoft.AspNetCore.Mvc;
using OnlyBot_Client.IServices;
using OnlyBot_Models;
using System.Net.Http.Json;

namespace OnlyBot_Client.Services
{
    public class BotService : IBotService
    {
        private readonly HttpClient _httpClient;

        public BotService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Create(Bot bot)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Bot", bot);

            response.EnsureSuccessStatusCode();
        }

        public async Task<int> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Bot/{id}");

            if (response.IsSuccessStatusCode)
            {
                return 1;
            }

            return 0;
        }

        public async Task<Bot> Get(Guid id)
        {
            var bot = await _httpClient.GetFromJsonAsync<Bot>($"api/Bot/{id}");

            if (bot != null)
            {
                return bot;
            }

            return new Bot();
        }

        public async Task<List<Bot>> GetAll()
        {
            var bots = await _httpClient.GetFromJsonAsync<List<Bot>>("api/Bot");

            if (bots != null)
            {
                return bots;
            }

            return new List<Bot>();
        }

        public async Task<List<Bot>> GetAllBotsByServer(string serverName)
        {
            var bots = await _httpClient.GetFromJsonAsync<List<Bot>>($"api/Bot/server/{serverName}");

            if (bots != null)
            {
                return bots;
            }

            return new List<Bot>();
        }

        public async Task<List<Bot>> GetFilteredBots(Dictionary<string, List<string>> filters)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Bot/filteredbots",filters);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<Bot>>();

                if (result == null)
                {
                    return new List<Bot>();
                }

                return result;
            }
            else
            {
                // Gérer les erreurs selon le besoin
                throw new HttpRequestException($"Erreur dans l'appel API: {response.ReasonPhrase}");
            }
        }

        public async Task Update(Bot bot)
        {
            var response = await _httpClient.PutAsJsonAsync<Bot>("api/Bot", bot);

            response.EnsureSuccessStatusCode();
        }
    }
}
