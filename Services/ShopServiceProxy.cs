using IVM.Library.Models;
using System.Collections.ObjectModel;

namespace IVM.Library.Services
{
    public class ShopServiceProxy
    {
        private static ShopServiceProxy? instance;
        private static readonly object instanceLock = new object();

        private List<ShoppingCart> carts;

        private ShopServiceProxy()
        {
            carts = new List<ShoppingCart>();
        }

        public static ShopServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    instance ??= new ShopServiceProxy();
                    return instance;
                }
            }
        }

        public ReadOnlyCollection<ShoppingCart> Carts => carts.AsReadOnly();

        public ShoppingCart Cart
        {
            get
            {
                if (!carts.Any())
                {
                    var newCart = new ShoppingCart();
                    carts.Add(newCart);
                    return newCart;
                }
                return carts.FirstOrDefault() ?? new ShoppingCart();
            }
        }

        public void AddToCart(Item newItem)
        {
            var cart = Cart;
            var existingItem = cart.Contents.FirstOrDefault(i => i.Id == newItem.Id);
            var inventoryItem = InventoryServiceProxy.Current.Items.FirstOrDefault(i => i.Id == newItem.Id);

            if (inventoryItem == null) return;

            inventoryItem.Quantity -= newItem.Quantity;

            if (existingItem != null)
            {
                existingItem.Quantity += newItem.Quantity;
            }
            else
            {
                cart.Contents.Add(newItem);
            }
        }

        public void RemoveFromCart(int itemId, int quantity)
        {
            var cart = Cart;
            var itemToRemove = cart.Contents.FirstOrDefault(i => i.Id == itemId);
            var inventoryItem = InventoryServiceProxy.Current.Items.FirstOrDefault(i => i.Id == itemId);

            if (itemToRemove != null)
            {
                if (itemToRemove.Quantity >= quantity)
                {
                    itemToRemove.Quantity -= quantity;
                    if (itemToRemove.Quantity == 0)
                    {
                        cart.Contents.Remove(itemToRemove);
                    }
                }
                else
                {
                    cart.Contents.Remove(itemToRemove);
                }

                if (inventoryItem != null)
                {
                    inventoryItem.Quantity += quantity;
                }
            }
        }

        public double TotalPrice => Cart.Contents.Sum(i => i.Price * i.Quantity);
    }
}

