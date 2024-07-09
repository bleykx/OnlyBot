using OnlyBot_Models;

namespace OnlyBot.IServices
{
    public interface IBotService
    {
        public Task<Bot> Create(Bot bot);
        public Task<Bot> Update(Bot bot);
        public Task<int> Delete(Guid id);
        public Task<Bot> Get(Guid id);
        public Task<List<Bot>> GetAll();
        public Task<List<Bot>> GetFilteredBots(Dictionary<string, List<string>> filters);
    }
}
