using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IVM.Library.Models;
using IVM.Library.Services;

namespace InventoryManagement.MAUI.ViewModels
{
    public class ShopViewModel : INotifyPropertyChanged
    {
        private readonly InventoryServiceProxy inventoryService;
        private readonly ShopServiceProxy shopService;

        public ShopViewModel()
        {
            inventoryService = InventoryServiceProxy.Current;
            shopService = ShopServiceProxy.Current;
            Items = new ObservableCollection<Item>(inventoryService.Items);
            CartItems = new ObservableCollection<(Item, int)>();
            AddToCartCommand = new Command(AddToCart);
            RemoveFromCartCommand = new Command(RemoveFromCart);
            SearchCommand = new Command(Search);
            InventoryQuery = string.Empty;
        }

        public ObservableCollection<Item> Items { get; private set; }
        public ObservableCollection<(Item, int)> CartItems { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (propertyName == nameof(CartItems))
            {
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public Command AddToCartCommand { get; }
        public Command RemoveFromCartCommand { get; }
        public Command SearchCommand { get; }

        private Item selectedItem;
        public Item SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        private (Item, int) selectedCartItem;
        public (Item, int) SelectedCartItem
        {
            get => selectedCartItem;
            set
            {
                selectedCartItem = value;
                OnPropertyChanged();
            }
        }

        private string inventoryQuery;
        public string InventoryQuery
        {
            get => inventoryQuery;
            set
            {
                inventoryQuery = value;
                OnPropertyChanged();
            }
        }

        private void AddToCart()
        {
            if (SelectedItem != null && SelectedItem.Quantity > 0)
            {
                shopService.AddToCart(new Item
                {
                    Id = SelectedItem.Id,
                    Name = SelectedItem.Name,
                    Description = SelectedItem.Description,
                    Price = SelectedItem.Price,
                    Quantity = 1
                });
                var cartItem = CartItems.FirstOrDefault(ci => ci.Item1.Id == SelectedItem.Id);
                if (cartItem.Item1 != null)
                {
                    CartItems.Remove(cartItem);
                    CartItems.Add((cartItem.Item1, cartItem.Item2 + 1));
                }
                else
                {
                    CartItems.Add((SelectedItem, 1));
                }
                SelectedItem.Quantity--;
                OnPropertyChanged(nameof(CartItems));
            }
        }

        private void RemoveFromCart()
        {
            if (SelectedCartItem.Item1 != null)
            {
                shopService.RemoveFromCart(SelectedCartItem.Item1.Id, 1);
                var cartItem = CartItems.FirstOrDefault(ci => ci.Item1.Id == SelectedCartItem.Item1.Id);
                if (cartItem.Item1 != null)
                {
                    if (cartItem.Item2 > 1)
                    {
                        CartItems.Remove(cartItem);
                        CartItems.Add((cartItem.Item1, cartItem.Item2 - 1));
                    }
                    else
                    {
                        CartItems.Remove(cartItem);
                    }
                }
                OnPropertyChanged(nameof(CartItems));
            }
        }

        public double TotalPrice => CartItems.Sum(c => c.Item1.Price * c.Item2);

        private void Search()
        {
            if (string.IsNullOrEmpty(InventoryQuery))
            {
                Items = new ObservableCollection<Item>(inventoryService.Items);
            }
            else
            {
                Items = new ObservableCollection<Item>(inventoryService.Items.Where(i => i.Name.Contains(InventoryQuery, StringComparison.OrdinalIgnoreCase)));
            }
            OnPropertyChanged(nameof(Items));
        }
    }
}
