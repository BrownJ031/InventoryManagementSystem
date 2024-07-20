using IVM.Library.Models;
using IVM.Library.Services;

namespace InventoryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var item = new Item
            {
                Name = "Sample Item",
                Description = "This is a sample item",
                Price = 10.0,
                Quantity = 100
            };

            InventoryServiceProxy.Current.AddOrUpdate(item);

            var cartItem = new Item(item)
            {
                Quantity = 1
            };

            ShopServiceProxy.Current.AddToCart(cartItem);

            Console.WriteLine($"Added {cartItem.Quantity} of {cartItem.Name} to cart.");
        }
    }
}

