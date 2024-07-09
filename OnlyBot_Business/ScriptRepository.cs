using Microsoft.EntityFrameworkCore;
using OnlyBot_Business.IRepository;
using OnlyBot_DataAccess;
using OnlyBot_Models;

namespace OnlyBot_Business
{
    public class ScriptRepository : IScriptRepository
    {
        private readonly ApplicationDbContext _context;

        public ScriptRepository(ApplicationDbContext context)
        {
            _context = context;
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
