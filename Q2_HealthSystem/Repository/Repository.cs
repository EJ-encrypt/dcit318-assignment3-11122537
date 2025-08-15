using System;
using System.Collections.Generic;
using System.Linq;

namespace Q2_HealthSystem.Repository
{
    public class Repository<T>
    {
        private readonly List<T> _items = new();

        // Add item
        public void Add(T item) => _items.Add(item);

        // Get all items
        public IEnumerable<T> GetAll() => _items.AsReadOnly();

        // Get first item matching predicate (or default/null)
        public T? GetById(Func<T, bool> predicate) => _items.FirstOrDefault(predicate);

        // Remove first item matching predicate; return true if removed
        public bool Remove(Func<T, bool> predicate)
        {
            var item = _items.FirstOrDefault(predicate);
            if (item == null) return false;
            return _items.Remove(item);
        }

        // Optional: count
        public int Count => _items.Count;
    }
}
