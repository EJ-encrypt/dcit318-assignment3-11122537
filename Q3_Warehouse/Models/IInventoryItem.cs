using System;

namespace Q3_Warehouse.Models
{
    // Simple marker + contract interface for inventory items
    public interface IInventoryItem
    {
        int Id { get; }
        string Name { get; }
        int Quantity { get; set; }   // mutable quantity for updates
        decimal UnitPrice { get; }
        string GetDisplayText();
    }
}
