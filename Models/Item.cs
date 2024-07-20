namespace InventoryManagementSystem.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public string? Display
        {
            get
            {
                return ToString();
            }
        }

        public Item()
        {
        }

        public override string ToString()
        {
            return $"[{Id}] {Name} - {Description} - ${Price} - Qty: {Quantity}";
        }
    }
}
