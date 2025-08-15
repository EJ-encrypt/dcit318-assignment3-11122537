using System;
using Q1_Finance.Models;

namespace Q1_Finance.Processors
{
    public class CryptoWalletProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            // Simulate crypto wallet processing (logging)
            Console.WriteLine($"[CryptoWallet] {transaction.Date:yyyy-MM-dd} | {transaction.Description} | {transaction.Amount:C} | {transaction.Category}");
        }
    }
}
