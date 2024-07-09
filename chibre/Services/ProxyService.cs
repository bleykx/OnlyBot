using OnlyBot_Client.IServices;
using OnlyBot_Models;

namespace OnlyBot_Client.Services
{
    public class ProxyService : IProxyService
    {
        public Task<Proxy> Create(Proxy proxy)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Proxy> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Proxy>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Proxy>> GetFilteredProxies(Dictionary<string, List<string>> filters)
        {
            throw new NotImplementedException();
        }

        public Task<Proxy> Update(Proxy proxy)
        {
            throw new NotImplementedException();
        }
    }
}
