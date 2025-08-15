using System;
using System.Collections.Generic;
using Q3_Warehouse.Models;
using Q3_Warehouse.Exceptions;

namespace Q3_Warehouse.Repository
{
    // Generic in-memory repository for inventory items.
    public class InventoryRepository<T> where T : IInventoryItem
    {
        private readonly Dictionary<int, T> _items = new();

        // Add item; throws DuplicateItemException if id already exists
        public void Add(T item)
        {
            if (_items.ContainsKey(item.Id))
                throw new DuplicateItemException($"An item with Id {item.Id} already exists.");

            _items[item.Id] = item;
        }

        // Get item by id (returns null if not found)
        public T? GetById(int id)
        {
            return _items.TryGetValue(id, out var item) ? item : default;
        }

        // Remove item by id; returns true if removed
        public bool RemoveById(int id)
        {
            return _items.Remove(id);
        }

        // Update quantity (throws ItemNotFoundException if not found)
        public void UpdateQuantity(int id, int newQuantity)
        {
            if (!_items.TryGetValue(id, out var item))
                throw new ItemNotFoundException($"Item with Id {id} not found.");

            item.Quantity = newQuantity;
        }

        // Get all items (shallow list)
        public IEnumerable<T> GetAll() => _items.Values;
    }
}
