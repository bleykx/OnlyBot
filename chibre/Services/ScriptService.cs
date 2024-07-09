using OnlyBot_Client.IServices;
using OnlyBot_Models;

namespace OnlyBot_Client.Services
{
    public class ScriptService : IScriptService
    {
        public Task<Script> Create(Script script)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Script> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Script>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Script>> GetFilteredScripts(Dictionary<string, List<string>> filters)
        {
            throw new NotImplementedException();
        }

        public Task<Script> Update(Script script)
        {
            throw new NotImplementedException();
        }
    }
}
