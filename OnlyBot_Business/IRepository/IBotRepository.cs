using OnlyBot_Models;

namespace OnlyBot_Business.IRepository
{
    public interface IBotRepository
    {
        public Task<Bot> Create(Bot Bot);
        public Task<Bot> Update(Bot Bot);
        public Task<int> Delete(Guid id);
        public Task<Bot> Get(Guid id);
        public Task<List<Bot>> GetAll();
        public Task<List<Bot>> GetAllBots();
        public Task<List<Bot>> GetAllBotsByServer(string serverName);
        public Task<List<Bot>> GetFilteredBots(Dictionary<string, List<string>> filters);
    }
}