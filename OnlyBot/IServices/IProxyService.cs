using Microsoft.CodeAnalysis.Scripting;
using OnlyBot_Models;

namespace OnlyBot.IServices
{
    public interface IProxyService
    {
        public Task<Proxy> Create(Proxy proxy);
        public Task<Proxy> Update(Proxy proxy);
        public Task<int> Delete(Guid id);
        public Task<Proxy> Get(Guid id);
        public Task<List<Proxy>> GetAll();
        public Task<List<Proxy>> GetFilteredProxies(Dictionary<string, List<string>> filters);
    }
}
