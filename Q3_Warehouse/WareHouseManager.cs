using System;
using System.Collections.Generic;
using Q3_Warehouse.Models;
using Q3_Warehouse.Repository;
using Q3_Warehouse.Exceptions;

namespace Q3_Warehouse
{
    // Manager that uses the repository and enforces business rules (throws our custom exceptions)
    public class WareHouseManager
    {
        private readonly InventoryRepository<IInventoryItem> _repo = new();

        public WareHouseManager() { }

        // Adds item; throws DuplicateItemException if id exists
        public void AddItem(IInventoryItem item)
        {
            // validate
            if (item.Quantity < 0)
                throw new InvalidQuantityException("Quantity cannot be negative.");

            _repo.Add(item); // repository will throw DuplicateItemException if duplicate
        }

        // Remove item; throws ItemNotFoundException if missing
        public void RemoveItem(int id)
        {
            var removed = _repo.RemoveById(id);
            if (!removed)
                throw new ItemNotFoundException($"Cannot remove: item with Id {id} not found.");
        }

        // Update quantity; throws ItemNotFoundException or InvalidQuantityException accordingly
        public void UpdateQuantity(int id, int newQuantity)
        {
            if (newQuantity < 0)
                throw new InvalidQuantityException("Quantity cannot be negative.");

            _repo.UpdateQuantity(id, newQuantity); // will throw ItemNotFoundException if missing
        }

        // Get item (returns null if not found)
        public IInventoryItem? GetItem(int id) => _repo.GetById(id);

        // List all
        public IEnumerable<IInventoryItem> ListAllItems() => _repo.GetAll();
    }
}
