namespace InventoryManagement.MAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnInventoryClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//InventoryPage");
        }

        private async void OnShopClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//ShopPage");
        }
    }
}
