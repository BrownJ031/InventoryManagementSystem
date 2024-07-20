using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IVM.Library.Models;
using IVM.Library.Services;

namespace InventoryManagement.MAUI.ViewModels
{
    public class InventoryViewModel : INotifyPropertyChanged
    {
        private readonly InventoryServiceProxy inventoryService;

        public InventoryViewModel()
        {
            inventoryService = InventoryServiceProxy.Current;
            Items = new ObservableCollection<Item>(inventoryService.Items);
            AddItemCommand = new Command(AddItem);
            UpdateItemCommand = new Command(UpdateItem);
            DeleteItemCommand = new Command(DeleteItem);
        }

        public ObservableCollection<Item> Items { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Command AddItemCommand { get; }
        public Command UpdateItemCommand { get; }
        public Command DeleteItemCommand { get; }

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

        private void AddItem()
        {
            var newItem = new Item { Name = "New Item", Description = "New Description", Price = 0, Quantity = 0 };
            inventoryService.AddOrUpdate(newItem);
            Items.Add(newItem);
        }

        private void UpdateItem()
        {
            if (SelectedItem != null)
            {
                inventoryService.AddOrUpdate(SelectedItem);
            }
        }

        private void DeleteItem()
        {
            if (SelectedItem != null)
            {
                inventoryService.Delete(SelectedItem.Id);
                Items.Remove(SelectedItem);
            }
        }
    }
}
