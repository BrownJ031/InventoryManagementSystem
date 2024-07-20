using IVM.Library.Models;
using System.Collections.ObjectModel;

namespace IVM.Library.Services
{
    public class InventoryServiceProxy
    {
        private static InventoryServiceProxy? instance;
        private static readonly object instanceLock = new object();
        private List<Item> items;

        private InventoryServiceProxy()
        {
            items = new List<Item>();
        }

        public static InventoryServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    instance ??= new InventoryServiceProxy();
                    return instance;
                }
            }
        }

        public ReadOnlyCollection<Item> Items => items.AsReadOnly();

        public Item AddOrUpdate(Item item)
        {
            var existingItem = items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                existingItem.Name = item.Name;
                existingItem.Description = item.Description;
                existingItem.Price = item.Price;
                existingItem.Quantity = item.Quantity;
            }
            else
            {
                item.Id = items.Any() ? items.Max(i => i.Id) + 1 : 1;
                items.Add(item);
            }
            return item;
        }

        public void Delete(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                items.Remove(item);
            }
        }

        public Item? GetItemById(int id)
        {
            return items.FirstOrDefault(i => i.Id == id);
        }
    }
}
