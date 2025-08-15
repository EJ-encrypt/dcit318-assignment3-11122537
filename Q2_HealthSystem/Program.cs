using System;

namespace Q2_HealthSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new HealthSystemApp();
            app.Run();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
