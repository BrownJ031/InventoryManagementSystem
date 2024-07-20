namespace IVM.Library.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public List<Item> Contents { get; set; }

        public ShoppingCart()
        {
            Contents = new List<Item>();
        }
    }
}
