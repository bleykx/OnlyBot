using OnlyBot_Models;

namespace OnlyBot_Client.IServices
{
    public interface IScriptService
    {
        public Task<Script> Create(Script script);
        public Task<Script> Update(Script script);
        public Task<int> Delete(Guid id);
        public Task<Script> Get(Guid id);
        public Task<List<Script>> GetAll();
        public Task<List<Script>> GetFilteredScripts(Dictionary<string, List<string>> filters);
    }
}
