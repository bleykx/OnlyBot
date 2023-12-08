using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OnlyBot_Business.Hubs;
using OnlyBot_Business.IRepository;
using OnlyBot_Business.UnitOfWork;
using OnlyBot_DataAccess;
using OnlyBot_Models;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace OnlyBot_Business
{
    public class ProxyRepository : IRepository<Script>
    {
        private readonly IHubContext<ProxiesHub> _hubContext;
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SqlTableDependency<Proxy> _dependency;

        public ProxyRepository(IUnitOfWork unitOfWork, ApplicationDbContext context, IHubContext<ProxiesHub> hubContext, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _hubContext = hubContext;
            _dependency = new SqlTableDependency<Proxy>(configuration.GetConnectionString("DefaultConnection"), "Proxies");
            _dependency.OnChanged += Changed;
            _dependency.Start();
        }

        private async void Changed(object sender, RecordChangedEventArgs<Proxy> e)
        {
            //var proxies = await _unitOfWork.Repository<Proxy>().GetAllEntitiesAsync();
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            await _hubContext.Clients.All.SendAsync("RefreshProxies", JsonConvert.SerializeObject(proxies, Formatting.Indented, settings));
        }

        public async Task Create(Proxy proxy)
        {
            //var addedProxy = await _context.Proxies.AddAsync(proxy);
            //await _context.SaveChangesAsync();
           
            //await _unitOfWork.Repository<Proxy>().AddEntityAsync(proxy);
            //await _unitOfWork.SaveChangesAsync();
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
                .Include(m => m.Bots)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (proxy != null)
            {
                return proxy;
            }

            return new Proxy();
        }

        //public async Task<List<Proxy>> GetAll()
        //{
        //    //return await _unitOfWork.Repository<Proxy>().GetAllEntitiesAsync();

        //     //return await _context.Proxies
        //     //   .AsNoTracking()
        //     //   .Include(m => m.Bots)
        //     //   .ToListAsync();
        //}

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

        public Script GetById(object id)
        {
            throw new NotImplementedException();
        }

        public void Add(Script entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Script entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }


        IList<Script> IRepository<Script>.GetAll()
        {
            
            
        }
    }
}