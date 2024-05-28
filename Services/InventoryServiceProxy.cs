using InventoryManagementSystem.Models;
using System.Collections.ObjectModel;

namespace InventoryManagementSystem.Library.Services
{
    public class InventoryServiceProxy
    {
        private InventoryServiceProxy()
        {
            items = new List<Item>();
        }

        private static InventoryServiceProxy? instance;
        private static object instanceLock = new object();
        public static InventoryServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new InventoryServiceProxy();
                    }
                }

                return instance;
            }
        }

        private List<Item>? items;
        public ReadOnlyCollection<Item>? Items
        {
            get
            {
                return items?.AsReadOnly();
            }
        }

        public int LastId
        {
            get
            {
                if (items?.Any() ?? false)
                {
                    return items?.Select(i => i.Id)?.Max() ?? 0;
                }
                return 0;
            }
        }

        public Item? AddOrUpdate(Item item)
        {
            if (items == null)
            {
                return null;
            }

            var existingItem = items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                existingItem.Name = item.Name ?? existingItem.Name;
                existingItem.Description = item.Description ?? existingItem.Description;
                existingItem.Price = item.Price != 0 ? item.Price : existingItem.Price;
                existingItem.Quantity = item.Quantity != 0 ? item.Quantity : existingItem.Quantity;
            }
            else
            {
                item.Id = LastId + 1;
                items.Add(item);
            }

            return item;
        }

        public void Delete(int id)
        {
            if (items == null)
            {
                return;
            }
            var itemToDelete = items.FirstOrDefault(i => i.Id == id);

            if (itemToDelete != null)
            {
                items.Remove(itemToDelete);
            }
        }

        public void ReadItems()
        {
            if (items == null || items.Count == 0)
            {
                Console.WriteLine("No items in inventory.");
                return;
            }

            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        public Item? GetItemById(int id)
        {
            return items?.FirstOrDefault(i => i.Id == id);
        }

        public void UpdateItem(int id, string? name, string? description, double? price, int? quantity)
        {
            var item = GetItemById(id);
            if (item != null)
            {
                if (!string.IsNullOrEmpty(name)) item.Name = name;
                if (!string.IsNullOrEmpty(description)) item.Description = description;
                if (price.HasValue) item.Price = price.Value;
                if (quantity.HasValue) item.Quantity = quantity.Value;
            }
            else
            {
                Console.WriteLine("Item not found.");
            }
        }
    }
}
