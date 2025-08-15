using System;
using Q1_Finance.Models;

namespace Q1_Finance.Processors
{
    public class BankTransferProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            // Simulate bank transfer processing (logging)
            Console.WriteLine($"[BankTransfer] {transaction.Date:yyyy-MM-dd} | {transaction.Description} | {transaction.Amount:C} | {transaction.Category}");
        }
    }
}
