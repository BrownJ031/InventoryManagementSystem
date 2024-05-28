using InventoryManagementSystem.Library.Services;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inventorySvc = InventoryServiceProxy.Current;
            var shop = new ShopServiceProxy(inventorySvc);

            while (true)
            {
                Console.WriteLine("1. Inventory Management\n2. Shop\n3. Exit");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    while (true)
                    {
                        Console.WriteLine("Inventory Management\n1. Create Item\n2. Read Items\n3. Update Item\n4. Delete Item\n5. Back");
                        string invChoice = Console.ReadLine();

                        if (invChoice == "1")
                        {
                            Console.Write("Enter item name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter item description: ");
                            string description = Console.ReadLine();
                            Console.Write("Enter item price: ");
                            double price = Convert.ToDouble(Console.ReadLine());
                            Console.Write("Enter item quantity: ");
                            int quantity = Convert.ToInt32(Console.ReadLine());
                            inventorySvc.AddOrUpdate(new Item { Name = name, Description = description, Price = price, Quantity = quantity });
                        }
                        else if (invChoice == "2")
                        {
                            inventorySvc.ReadItems();
                        }
                        else if (invChoice == "3")
                        {
                            Console.Write("Enter item ID to update: ");
                            int itemId = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter new name (leave blank to keep current): ");
                            string name = Console.ReadLine();
                            Console.Write("Enter new description (leave blank to keep current): ");
                            string description = Console.ReadLine();
                            Console.Write("Enter new price (leave blank to keep current): ");
                            string priceInput = Console.ReadLine();
                            Console.Write("Enter new quantity (leave blank to keep current): ");
                            string quantityInput = Console.ReadLine();
                            double? price = string.IsNullOrEmpty(priceInput) ? (double?)null : Convert.ToDouble(priceInput);
                            int? quantity = string.IsNullOrEmpty(quantityInput) ? (int?)null : Convert.ToInt32(quantityInput);
                            inventorySvc.UpdateItem(itemId, name, description, price, quantity);
                        }
                        else if (invChoice == "4")
                        {
                            Console.Write("Enter item ID to delete: ");
                            int itemId = Convert.ToInt32(Console.ReadLine());
                            inventorySvc.Delete(itemId);
                        }
                        else if (invChoice == "5")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice.");
                        }
                    }
                }
                else if (choice == "2")
                {
                    while (true)
                    {
                        Console.WriteLine("Shop\n1. Add to Cart\n2. Remove from Cart\n3. Checkout\n4. Back");
                        string shopChoice = Console.ReadLine();

                        if (shopChoice == "1")
                        {
                            Console.Write("Enter item ID to add to cart: ");
                            int itemId = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter quantity: ");
                            int quantity = Convert.ToInt32(Console.ReadLine());
                            shop.AddToCart(itemId, quantity);
                        }
                        else if (shopChoice == "2")
                        {
                            Console.Write("Enter item ID to remove from cart: ");
                            int itemId = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter quantity: ");
                            int quantity = Convert.ToInt32(Console.ReadLine());
                            shop.RemoveFromCart(itemId, quantity);
                        }
                        else if (shopChoice == "3")
                        {
                            shop.Checkout();
                        }
                        else if (shopChoice == "4")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice.");
                        }
                    }
                }
                else if (choice == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }
            }
        }
    }
}
