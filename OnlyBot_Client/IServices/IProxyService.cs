using OnlyBot_Models;

namespace OnlyBot_Client.IServices
{
    public interface IProxyService
    {
        public Task Create(Proxy proxy);
        public Task Update(Proxy proxy);
        public Task<int> Delete(Guid id);
        public Task<Proxy> Get(Guid id);
        public Task<List<Proxy>> GetAll();
        public Task<List<Proxy>> GetFilteredProxies(Dictionary<string, List<string>> filters);
    }
}
