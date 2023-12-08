using OnlyBot_Models;

namespace OnlyBot_Business.IRepository
{
    public interface IScriptRepository
    {
        public Task<Script> Create(Script Script);
        public Task<Script> Update(Script Script);
        public Task<int> Delete(Guid id);
        public Task<Script> Get(Guid id);
        public Task<List<Script>> GetAll();
        public Task<List<Script>> GetFilteredScripts(Dictionary<string, List<string>> filters);
    }
}
