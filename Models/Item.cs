namespace IVM.Library.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public string Display => $"{Name} - {Description} - ${Price} - Qty: {Quantity}";

        public Item() { }

        public Item(Item item)
        {
            Id = item.Id;
            Name = item.Name;
            Description = item.Description;
            Price = item.Price;
            Quantity = item.Quantity;
        }

        public override string ToString()
        {
            return Display;
        }
    }
}
