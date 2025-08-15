// Q5_InventoryLogger/Program.cs
using System;

namespace Q5_InventoryLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            var storagePath = args.Length > 0 ? args[0] : "inventory.json";

            var app = new InventoryApp(storagePath);
            app.Run();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
