using MyCrudApi.Models;
using System.Collections.Concurrent;

namespace MyCrudApi.Repositories;

public class InMemoryItemRepository
{
    private static readonly ConcurrentDictionary<string, Item> _items = new();

    public IEnumerable<Item> GetAll() => _items.Values;
    public Item? Get(string id) => _items.GetValueOrDefault(id);
    public void Add(Item item) => _items[item.Id] = item;
    public bool Update(string id, Item updatedItem)
    {
        if (!_items.ContainsKey(id)) return false;
        updatedItem.Id = id;
        _items[id] = updatedItem;
        return true;
    }
    public bool Delete(string id) => _items.TryRemove(id, out _);
}
