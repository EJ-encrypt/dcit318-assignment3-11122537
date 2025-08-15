using System;
using Q3_Warehouse;
using Q3_Warehouse.Models;
using Q3_Warehouse.Exceptions;

namespace Q3_Warehouse
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new WareHouseManager();

            Console.WriteLine("=== Q3_Warehouse Demo ===\n");

            // 1) Add two valid items
            try
            {
                var e1 = new ElectronicItem(1, "Bluetooth Speaker", 10, 59.99m, 12);
                manager.AddItem(e1);
                Console.WriteLine("Added: " + e1.GetDisplayText());

                var g1 = new GroceryItem(2, "Rice 5kg", 50, 12.50m, DateTime.Today.AddMonths(6));
                manager.AddItem(g1);
                Console.WriteLine("Added: " + g1.GetDisplayText());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error adding initial items: " + ex.Message);
            }

            Console.WriteLine();

            // 2) Attempt to add a duplicate item (should throw DuplicateItemException)
            try
            {
                var duplicate = new ElectronicItem(1, "Another Speaker", 5, 49.99m, 6);
                Console.WriteLine("Attempting to add duplicate item with Id=1...");
                manager.AddItem(duplicate);
            }
            catch (DuplicateItemException dex)
            {
                Console.WriteLine("DuplicateItemException caught: " + dex.Message);
            }

            Console.WriteLine();

            // 3) Attempt to remove a non-existent item (should throw ItemNotFoundException)
            try
            {
                Console.WriteLine("Attempting to remove item with Id=99 (non-existent)...");
                manager.RemoveItem(99);
            }
            catch (ItemNotFoundException inf)
            {
                Console.WriteLine("ItemNotFoundException caught: " + inf.Message);
            }

            Console.WriteLine();

            // 4) Attempt to update quantity with invalid value (negative -> InvalidQuantityException)
            try
            {
                Console.WriteLine("Attempting to set quantity for item Id=2 to -5...");
                manager.UpdateQuantity(2, -5);
            }
            catch (InvalidQuantityException iqe)
            {
                Console.WriteLine("InvalidQuantityException caught: " + iqe.Message);
            }

            Console.WriteLine();

            // 5) Do a normal update and then list all items
            try
            {
                Console.WriteLine("Setting quantity for item Id=2 to 30 (valid)...");
                manager.UpdateQuantity(2, 30);
                Console.WriteLine("Update successful.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error updating quantity: " + ex.Message);
            }

            // 6) List items
            Console.WriteLine("Current inventory:");
            foreach (var item in manager.ListAllItems())
            {
                Console.WriteLine(" - " + item.GetDisplayText());
            }

            Console.WriteLine("\nDemo complete. Press any key to exit...");
            Console.ReadKey();
        }
    }
}
