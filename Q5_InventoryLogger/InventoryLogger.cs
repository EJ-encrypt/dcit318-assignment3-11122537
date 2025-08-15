// Q5_InventoryLogger/InventoryLogger.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Q5_InventoryLogger.Models;

namespace Q5_InventoryLogger
{
    /// <summary>
    /// Generic immutable inventory logger. Keeps a history (append-only) of item versions.
    /// To update an item you must provide an updateFactory which knows how to create a new T from an existing T and a new quantity.
    /// </summary>
    public class InventoryLogger<T> where T : IInventoryEntity
    {
        private readonly List<T> _history = new();
        private readonly Func<T, int, T> _updateFactory;
        private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

        /// <summary>
        /// constructor requires an update factory for creating new versions of T when quantity changes.
        /// Example for InventoryItem: (item, q) => item with { Quantity = q, CreatedAt = DateTime.UtcNow }
        /// </summary>
        /// <param name="updateFactory">Function that given an existing item and new quantity returns a new T instance</param>
        public InventoryLogger(Func<T, int, T> updateFactory)
        {
            _updateFactory = updateFactory ?? throw new ArgumentNullException(nameof(updateFactory));
        }

        /// <summary>
        /// Adds a new item version. If an item with same Id already exists (latest snapshot), throws InvalidOperationException.
        /// </summary>
        public void Add(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            // If latest snapshot contains this id, do not allow duplicate initial add
            if (GetLatestById(item.Id) != null)
                throw new InvalidOperationException($"An item with Id {item.Id} already exists.");

            _history.Add(item);
        }

        /// <summary>
        /// Returns the latest version of item with given id or null if missing.
        /// </summary>
        public T? GetLatestById(int id)
        {
            // Last occurrence of that id in history is the latest version
            return _history.LastOrDefault(i => i.Id == id);
        }

        /// <summary>
        /// Returns latest snapshot for all items (one entry per unique id).
        /// </summary>
        public IEnumerable<T> GetAllLatest()
        {
            // Group by id and take the last element in each group (last appended)
            return _history
                .GroupBy(i => i.Id)
                .Select(g => g.Last())
                .OrderBy(i => i.Id)
                .ToList();
        }

        /// <summary>
        /// Update quantity immutably: creates a new version of the item using the provided updateFactory and appends it.
        /// Throws InvalidOperationException if item not found or if newQuantity invalid.
        /// </summary>
        public void UpdateQuantity(int id, int newQuantity)
        {
            if (newQuantity < 0) throw new ArgumentOutOfRangeException(nameof(newQuantity), "Quantity cannot be negative.");

            var latest = GetLatestById(id);
            if (latest == null)
                throw new InvalidOperationException($"Cannot update: item with Id {id} not found.");

            var newItem = _updateFactory(latest, newQuantity);
            if (newItem == null)
                throw new InvalidOperationException("Update factory returned null.");

            _history.Add(newItem);
        }

        /// <summary>
        /// Saves the current latest snapshot of all items to JSON file (overwrites).
        /// </summary>
        public void SaveToFile(string path)
        {
            var snapshot = GetAllLatest().ToList();
            var json = JsonSerializer.Serialize(snapshot, _jsonOptions);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Loads items from JSON file and replaces the internal history with the loaded items (each treated as the latest version).
        /// If file doesn't exist it returns false.
        /// </summary>
        public bool LoadFromFile(string path)
        {
            if (!File.Exists(path)) return false;

            var json = File.ReadAllText(path);
            var loaded = JsonSerializer.Deserialize<List<T>>(json, _jsonOptions) ?? new List<T>();

            // Replace history with the loaded items (treat each as a single version)
            _history.Clear();
            _history.AddRange(loaded);
            return true;
        }

        /// <summary>
        /// Clears in-memory history (useful to simulate new session).
        /// </summary>
        public void ClearHistory() => _history.Clear();

        /// <summary>
        /// Seed sample data by calling Add for each item. Throws on duplicate ids.
        /// </summary>
        public void SeedSampleData(IEnumerable<T> sampleItems)
        {
            foreach (var item in sampleItems)
                Add(item);
        }
    }
}
