namespace InventoryManagement.MAUI.ViewModels
{
    public class MainViewModel
    {
        public Command NavigateToInventoryCommand { get; }
        public Command NavigateToShopCommand { get; }

        public MainViewModel()
        {
            NavigateToInventoryCommand = new Command(() => Application.Current.MainPage.Navigation.PushAsync(new Views.InventoryPage()));
            NavigateToShopCommand = new Command(() => Application.Current.MainPage.Navigation.PushAsync(new Views.ShopPage()));
        }
    }
}
