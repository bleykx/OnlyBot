using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OnlyBot_Business.Hubs;
using OnlyBot_Business.IRepository;
using OnlyBot_DataAccess;
using OnlyBot_Models;
using OnlyBot_Models.Enums;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace OnlyBot_Business
{
    public class BotRepository : IBotRepository
    {
        private readonly IHubContext<BotsHub> _hubContext;
        private readonly ApplicationDbContext _context;
        private readonly SqlTableDependency<Bot> _dependency;

        public BotRepository(ApplicationDbContext context, IHubContext<BotsHub> hubContext, IConfiguration configuration)
        {
            _context = context;
            _hubContext = hubContext;
            _dependency = new SqlTableDependency<Bot>(configuration.GetConnectionString("DefaultConnection"), "Bots");
            _dependency.OnChanged += Changed;
            _dependency.Start();
        }

        private async void Changed(object sender, RecordChangedEventArgs<Bot> e)
        {
            var bots = await GetAll();
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            await _hubContext.Clients.All.SendAsync("RefreshBots", JsonConvert.SerializeObject(bots, Formatting.Indented, settings));
        }

        public async Task<Bot> Create(Bot bot)
        {
            bot.ScriptId = bot.Script != null ? bot.Script.Id :  null;
            bot.Script = null;
            bot.ProxyId = bot.Proxy != null ? bot.Proxy.Id : null;
            bot.Proxy = null;
            bot.Inventories = null;
            bot.Order = null;
            var addedBot = await _context.Bots.AddAsync(bot);
            await _context.SaveChangesAsync();

            return addedBot.Entity;
        }

        public async Task<int> Delete(Guid id)
        {
            var bot = await _context.Bots.FirstOrDefaultAsync(b => b.Id == id);

            if (bot != null)
            {
                _context.Bots.Remove(bot);
                return await _context.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<Bot> Get(Guid botId)
        {
            var bot = await _context.Bots
                .AsNoTracking()
                .Include(b => b.Script)
                .Include(b => b.Inventories)
                .ThenInclude(i => i.Items)
                .FirstOrDefaultAsync(b => b.Id == botId);

            if (bot != null)
            {
                return bot;
            }

            return new Bot();
        }


        public async Task<List<Bot>> GetAllBots()
        {
            return await _context.Bots
                .AsNoTracking()
                .Include(b => b.Script)
                .Include(b => b.Inventories)
                .ToListAsync();
        }

        public async Task<List<Bot>> GetAll()
        {
            var bots = await _context.Bots
                .AsNoTracking()
                .Include(b => b.Script)
                .Include(b => b.Inventories)
                .ToListAsync();

            return bots;
        }

        public async Task<List<Bot>> GetAllBotsByServer(string serverName)
        {
            var server = EnumHelper.GetServerEnumFromString(serverName);

            var botsByServer = await _context.Bots
                .AsNoTracking()
                .Where(b => b.Server == server)
                .Include(b => b.Script)
                .Include(b => b.Inventories)
                .ThenInclude(i => i.Items)
                .ToListAsync();

            return botsByServer;
        }

        public async Task<Bot> Update(Bot Bot)
        {
            var botFromDb = await _context.Bots.FirstOrDefaultAsync(b => b.Id == Bot.Id);

            if (botFromDb != null)
            {
                botFromDb.AnkabotInstance = Bot.AnkabotInstance;
                botFromDb.AccountName = Bot.AccountName;
                botFromDb.Name = Bot.Name;
                botFromDb.Description = Bot.Description;
                botFromDb.Breed = Bot.Breed;
                botFromDb.BreedImgLink = Bot.BreedImgLink;
                botFromDb.Script = Bot.Script;
                botFromDb.ScriptIsRunning = Bot.ScriptIsRunning;
                botFromDb.IsConnected = Bot.IsConnected;
                botFromDb.Level = Bot.Level;
                botFromDb.Life = Bot.Life;
                botFromDb.MaxLife = Bot.MaxLife;
                botFromDb.Energy = Bot.Energy;
                botFromDb.MaxEnergy = Bot.MaxEnergy;
                botFromDb.Experience = Bot.Experience;
                botFromDb.ExperienceFloor = Bot.ExperienceFloor;
                botFromDb.ExperienceNextFloor = Bot.ExperienceNextFloor;
                botFromDb.ExperiencePercent = Bot.ExperiencePercent;

                await _context.SaveChangesAsync();

                return botFromDb;
            }

            return Bot;
        }

        public async Task<List<Bot>> GetFilteredBots(Dictionary<string, List<string>> filters)
        {
            var bots = await GetAllBots();

            if (filters.Count == 0)
            {
                return bots;
            }

            foreach (var filter in filters)
            {
                switch (filter.Key)
                {
                    case "Server":
                        bots = bots.Where(b => filter.Value.Contains(b.Server.ToString())).ToList();
                        break;
                    case "Script":
                        bots = bots.Where(m => m.Script != null).Where(b => filter.Value.Contains(b.Script.Type.ToString())).ToList();
                        break;
                    case "Breed":
                        bots = bots.Where(b => filter.Value.Contains(b.Breed.ToString())).ToList();
                        break;
                    case "ScriptIsRunning":
                        bots = bots.Where(b => filter.Value.Contains(b.ScriptIsRunning.ToString())).ToList();
                        break;
                    case "IsConnected":
                        bots = bots.Where(b => filter.Value.Contains(b.IsConnected.ToString())).ToList();
                        break;
                    default:
                        break;
                }
            }

            return bots;
        }
    }
}
