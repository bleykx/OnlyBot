using OnlyBot_Client.IServices;
using OnlyBot_Models;

namespace OnlyBot_Client.Services
{
    public class BotService : IBotService
    {
        public Task<Bot> Create(Bot bot)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Bot> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Bot>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Bot>> GetFilteredBots(Dictionary<string, List<string>> filters)
        {
            throw new NotImplementedException();
        }

        public Task<Bot> Update(Bot bot)
        {
            throw new NotImplementedException();
        }
    }
}
