using OnlyBot_Models;

namespace OnlyBot_Client.IServices
{
    public interface IBotService
    {
        public Task Create(Bot bot);
        public Task Update(Bot bot);
        public Task<int> Delete(Guid id);
        public Task<Bot> Get(Guid id);
        public Task<List<Bot>> GetAll();
        public Task<List<Bot>> GetFilteredBots(Dictionary<string, List<string>> filters);
        public Task<List<Bot>> GetAllBotsByServer(string serverName);
    }
}