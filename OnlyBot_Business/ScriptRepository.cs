using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OnlyBot_Business.Hubs;
using OnlyBot_Business.IRepository;
using OnlyBot_DataAccess;
using OnlyBot_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace OnlyBot_Business
{
    public class ScriptRepository : IScriptRepository
    {
        private readonly IHubContext<ScriptHub> _hubContext;
        private readonly ApplicationDbContext _context;
        private readonly SqlTableDependency<Script> _dependency;
        public ScriptRepository(ApplicationDbContext context, IHubContext<ScriptHub> hubContext, IConfiguration configuration)
        {
            _context = context;
            _hubContext = hubContext;
            _dependency = new SqlTableDependency<Script>(configuration.GetConnectionString("DefaultConnection"), "Scripts");
            _dependency.OnChanged += Changed;
            _dependency.Start();
        }

        private async void Changed(object sender, RecordChangedEventArgs<Script> e)
        {
            var scripts = await GetAll();
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            await _hubContext.Clients.All.SendAsync("RefreshScripts", JsonConvert.SerializeObject(scripts, Formatting.Indented, settings));
        }

        public async Task<Script> Create(Script Script)
        {
            var script = Script;

            var addedScript = await _context.Scripts.AddAsync(script);
            await _context.SaveChangesAsync();

            return addedScript.Entity;
        }

        public async Task<int> Delete(Guid id)
        {
            var script = await _context.Scripts.FirstOrDefaultAsync(i => i.Id == id);

            if (script != null)
            {
                _context.Scripts.Remove(script);
                return await _context.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<Script> Get(Guid id)
        {
            var script = await _context.Scripts.FirstOrDefaultAsync(i => i.Id == id);

            if (script != null)
            {
                return script;
            }

            return new Script();
        }

        public async Task<List<Script>> GetAll()
        {
            return await _context.Scripts.AsNoTracking().ToListAsync();
        }

        public async Task<Script> Update(Script Script)
        {
            var scriptFromDb = await _context.Scripts.FirstOrDefaultAsync(i => i.Id == Script.Id);

            if(scriptFromDb != null)
            {
                scriptFromDb.Id = Script.Id;
                scriptFromDb.Name = Script.Name;
                scriptFromDb.Description = Script.Description;
                scriptFromDb.Path = Script.Path;
                scriptFromDb.Type = Script.Type;

                await _context.SaveChangesAsync();
                return scriptFromDb;
            }

            return Script;
        }

        public async Task<List<Script>> GetFilteredScripts(Dictionary<string, List<string>> filters)
        {
            var scripts = await GetAll();

            if (filters.Count == 0)
            {
                return scripts;
            }

            foreach (var filter in filters)
            {
                switch (filter.Key)
                {
                    case "Type":
                        scripts = scripts.Where(b => filter.Value.Contains(b.Type.ToString())).ToList();
                        break;
                    case "Name":
                        scripts = scripts.Where(b => filter.Value.Contains(b.Name!)).ToList();
                        break;
                    default:
                        break;
                }
            }

            return scripts;
        }
    }
}
