using OnlyBot_Models;

namespace OnlyBot_Business.IRepository
{
    public interface IProxyRepository
    {
        public Task<Proxy> Create(Proxy proxy);
        public Task<Proxy> Update(Proxy proxy);
        public Task<int> Delete(Guid id);
        public Task<Proxy> Get(Guid id);
        public Task<List<Proxy>> GetAll();
        public Task<List<Proxy>> GetFilteredProxies(Dictionary<string, List<string>> filters);
    }
}