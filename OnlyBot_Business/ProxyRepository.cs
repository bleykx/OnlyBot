using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlyBot_Business.IRepository;
using OnlyBot_DataAccess;
using OnlyBot_Models;

namespace OnlyBot_Business
{
    public class ProxyRepository : IProxyRepository
    {
        private readonly ApplicationDbContext _context;

        public ProxyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Proxy> Create(Proxy proxy)
        {
            var addedProxy = await _context.Proxies.AddAsync(proxy);
            await _context.SaveChangesAsync();

            return addedProxy.Entity;
        }

        public async Task<int> Delete(Guid id)
        {
            var proxy = await _context.Proxies.FirstOrDefaultAsync(p => p.Id == id);

            if (proxy != null)
            {
                _context.Proxies.Remove(proxy);
                return await _context.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<Proxy> Get(Guid id)
        {
            var proxy = await _context.Proxies
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (proxy != null)
            {
                return proxy;
            }

            return new Proxy();
        }

        public async Task<List<Proxy>> GetAll()
        {
            return await _context.Proxies
               .AsNoTracking()
               .ToListAsync();
        }

        public async Task<Proxy> Update(Proxy Proxy)
        {
            var proxyFromDb = await _context.Proxies.FirstOrDefaultAsync(p => p.Id == Proxy.Id);

            if (proxyFromDb != null)
            {
                proxyFromDb.Id = Proxy.Id;
                proxyFromDb.IP = Proxy.IP;
                proxyFromDb.Port = Proxy.Port;
                proxyFromDb.Username = Proxy.Username;
                proxyFromDb.Password = Proxy.Password;
                proxyFromDb.Bots = Proxy.Bots;
                proxyFromDb.IsBanned = Proxy.IsBanned;

                await _context.SaveChangesAsync();

                return proxyFromDb;
            }

            return Proxy;
        }

        public async Task<List<Proxy>> GetFilteredProxies(Dictionary<string, List<string>> filters)
        {
            var proxies = await GetAll();

            if (filters.Count == 0)
            {
                return proxies;
            }

            foreach (var filter in filters)
            {
                switch (filter.Key)
                {
                    case "IsBanned":
                        proxies = proxies.Where(b => filter.Value.Contains(b.IsBanned.ToString())).ToList();
                        break;
                    case "Provider":
                        proxies = proxies.Where(b => filter.Value.Contains(b.Provider!)).ToList();
                        break;
                    default:
                        break;
                }
            }

            return proxies;
        }

        public Proxy GetById(object id)
        {
            throw new NotImplementedException();
        }                
    }
}