using System;

namespace Q3_Warehouse.Models
{
    public class ElectronicItem : IInventoryItem
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; init; }
        public int WarrantyMonths { get; init; }

        public ElectronicItem() { }

        public ElectronicItem(int id, string name, int qty, decimal price, int warrantyMonths)
        {
            Id = id;
            Name = name;
            Quantity = qty;
            UnitPrice = price;
            WarrantyMonths = warrantyMonths;
        }

        public string GetDisplayText()
        {
            return $"[Electronic] Id={Id}, Name={Name}, Qty={Quantity}, Price={UnitPrice:C}, Warranty={WarrantyMonths} months";
        }
    }
}
