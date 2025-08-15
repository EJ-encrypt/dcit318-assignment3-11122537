// Q5_InventoryLogger/Models/IInventoryEntity.cs
using System;

namespace Q5_InventoryLogger.Models
{
    /// <summary>
    /// Minimal contract for inventory items used by InventoryLogger.
    /// </summary>
    public interface IInventoryEntity
    {
        int Id { get; }
        string Name { get; }
        int Quantity { get; }
        decimal UnitPrice { get; }
        DateTime CreatedAt { get; }

        /// <summary>
        /// Returns a short display string for UI/console.
        /// </summary>
        string GetDisplayText();
    }
}
