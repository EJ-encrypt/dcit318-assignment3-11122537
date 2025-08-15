// Q5_InventoryLogger/InventoryApp.cs
using System;
using System.Collections.Generic;
using System.IO;
using Q5_InventoryLogger.Models;

namespace Q5_InventoryLogger
{
    /// <summary>
    /// Demonstration app for the InventoryLogger using InventoryItem.
    /// </summary>
    public class InventoryApp
    {
        private readonly InventoryLogger<InventoryItem> _logger;
        private readonly string _storagePath;

        public InventoryApp(string storagePath = "inventory.json")
        {
            // Provide an updateFactory that creates a new InventoryItem with a changed quantity and updated CreatedAt
            _logger = new InventoryLogger<InventoryItem>((existing, newQty) =>
            {
                // Use 'with' expression to create a new record from existing one
                return existing with { Quantity = newQty, CreatedAt = DateTime.UtcNow };
            });

            _storagePath = storagePath;
        }

        /// <summary>
        /// Run the demo: seed data, save to file, clear, reload, update an item, print
        /// </summary>
        public void Run()
        {
            Console.WriteLine("=== Q5_InventoryLogger Demo ===\n");

            SeedSampleData();

            Console.WriteLine("Saving current inventory to file...");
            _logger.SaveToFile(_storagePath);
            Console.WriteLine($"Saved to: {Path.GetFullPath(_storagePath)}\n");

            Console.WriteLine("Clearing in-memory logger and reloading from file to verify persistence...");
            _logger.ClearHistory();
            var loaded = _logger.LoadFromFile(_storagePath);
            Console.WriteLine(loaded ? "Load successful." : "Load failed (file missing).");
            PrintAllItems();

            Console.WriteLine();

            // Demonstrate immutable update (quantity change) - pick an existing id
            var first = _logger.GetAllLatest().FirstOrDefault();
            if (first != null)
            {
                Console.WriteLine($"Updating quantity of item Id={first.Id} (current qty={first.Quantity}) to {first.Quantity + 5}...");
                _logger.UpdateQuantity(first.Id, first.Quantity + 5);
                Console.WriteLine("Update recorded as a new immutable version.\n");
            }

            PrintAllItems();

            Console.WriteLine("\nSaving updated inventory to file...");
            _logger.SaveToFile(_storagePath);
            Console.WriteLine("Done.\n");
        }

        public void SeedSampleData()
        {
            // Only seed if logger is empty (avoid duplicates)
            if (_logger.GetAllLatest().Any()) return;

            var samples = new List<InventoryItem>
            {
                new InventoryItem(1, "USB-C Cable", 30, 4.99m, "Accessories"),
                new InventoryItem(2, "Wireless Mouse", 15, 19.99m, "Peripherals"),
                new InventoryItem(3, "Laptop 14\" Model X", 5, 899.00m, "Electronics"),
            };

            _logger.SeedSampleData(samples);
            Console.WriteLine("Seeded sample data:");
            PrintAllItems();
            Console.WriteLine();
        }

        public void PrintAllItems()
        {
            Console.WriteLine("Current inventory (latest snapshot):");
            foreach (var item in _logger.GetAllLatest())
            {
                Console.WriteLine(" - " + item.GetDisplayText());
            }
        }

        // Expose logger (for tests or interactive uses)
        public InventoryLogger<InventoryItem> Logger => _logger;
    }
}
