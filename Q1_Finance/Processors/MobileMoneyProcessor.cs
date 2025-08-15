using System;
using Q1_Finance.Models;

namespace Q1_Finance.Processors
{
    public class MobileMoneyProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            // Simulate mobile money processing (logging)
            Console.WriteLine($"[MobileMoney] {transaction.Date:yyyy-MM-dd} | {transaction.Description} | {transaction.Amount:C} | {transaction.Category}");
        }
    }
}
