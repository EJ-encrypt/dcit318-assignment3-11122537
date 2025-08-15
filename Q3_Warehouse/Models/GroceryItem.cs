using System;

namespace Q3_Warehouse.Models
{
    public class GroceryItem : IInventoryItem
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; init; }
        public DateTime ExpiryDate { get; init; }

        public GroceryItem() { }

        public GroceryItem(int id, string name, int qty, decimal price, DateTime expiry)
        {
            Id = id;
            Name = name;
            Quantity = qty;
            UnitPrice = price;
            ExpiryDate = expiry;
        }

        public string GetDisplayText()
        {
            return $"[Grocery] Id={Id}, Name={Name}, Qty={Quantity}, Price={UnitPrice:C}, Expires={ExpiryDate:d}";
        }
    }
}
