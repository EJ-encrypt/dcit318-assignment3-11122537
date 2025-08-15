using System;

namespace Q1_Finance
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new FinanceApp();
            app.Run();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
