using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Library.Services
{
    public class ShopServiceProxy
    {
        private InventoryServiceProxy inventory;
        private List<(Item, int)> cart;

        public ShopServiceProxy(InventoryServiceProxy inventory)
        {
            this.inventory = inventory;
            cart = new List<(Item, int)>();
        }

        public void AddToCart(int itemId, int quantity)
        {
            var item = inventory.GetItemById(itemId);
            if (item != null)
            {
                if (item.Quantity >= quantity)
                {
                    cart.Add((item, quantity));
                    item.Quantity -= quantity;
                    Console.WriteLine($"{quantity} of {item.Name} added to cart.");
                }
                else
                {
                    Console.WriteLine("Not enough quantity available.");
                }
            }
            else
            {
                Console.WriteLine("Item not found.");
            }
        }

        public void RemoveFromCart(int itemId, int quantity)
        {
            var cartItem = cart.FirstOrDefault(ci => ci.Item1.Id == itemId);
            if (cartItem.Item1 != null)
            {
                if (cartItem.Item2 >= quantity)
                {
                    cartItem.Item1.Quantity += quantity;
                    if (cartItem.Item2 == quantity)
                    {
                        cart.Remove(cartItem);
                    }
                    else
                    {
                        cart.Remove(cartItem);
                        cart.Add((cartItem.Item1, cartItem.Item2 - quantity));
                    }
                    Console.WriteLine($"{quantity} of {cartItem.Item1.Name} removed from cart.");
                }
                else
                {
                    Console.WriteLine("Not enough quantity in cart.");
                }
            }
            else
            {
                Console.WriteLine("Item not found in cart.");
            }
        }

        public void Checkout()
        {
            if (cart.Count == 0)
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            double subtotal = 0;
            foreach (var cartItem in cart)
            {
                subtotal += cartItem.Item1.Price * cartItem.Item2;
            }
            double tax = subtotal * 0.07;
            double total = subtotal + tax;

            Console.WriteLine("Receipt:");
            foreach (var cartItem in cart)
            {
                Console.WriteLine($"{cartItem.Item1.Name} (x{cartItem.Item2}): ${cartItem.Item1.Price * cartItem.Item2:F2}");
            }
            Console.WriteLine($"Subtotal: ${subtotal:F2}");
            Console.WriteLine($"Tax: ${tax:F2}");
            Console.WriteLine($"Total: ${total:F2}");

            cart.Clear();
        }
    }
}
