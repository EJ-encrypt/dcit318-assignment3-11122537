// Q5_InventoryLogger/Models/InventoryItem.cs
using System;

namespace Q5_InventoryLogger.Models
{
    /// <summary>
    /// Immutable inventory item implemented as a record.
    /// Use `with` expressions to produce modified copies.
    /// </summary>
    public record InventoryItem : IInventoryEntity
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public int Quantity { get; init; }
        public decimal UnitPrice { get; init; }
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
        public string Category { get; init; } = string.Empty;

        public InventoryItem() { }

        public InventoryItem(int id, string name, int quantity, decimal unitPrice, string category = "")
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Category = category;
            CreatedAt = DateTime.UtcNow;
        }

        public string GetDisplayText()
        {
            return $"Id={Id}, Name={Name}, Qty={Quantity}, Price={UnitPrice:C}, Category={Category}, Created={CreatedAt:u}";
        }
    }
}
