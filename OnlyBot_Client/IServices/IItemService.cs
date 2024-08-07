﻿using OnlyBot_Models;

namespace OnlyBot_Client.IServices
{
    public interface IItemService
    {
        public Task Create(Item item);
        public Task Update(Item item);
        public Task<int> Delete(Guid id);
        public Task<Item> Get(Guid id);
        public Task<List<Item>> GetAll();
        public Task<List<Item>> GetFilteredItems(Dictionary<string, List<string>> filters);
    }
}
