using System;
using System.Collections.Generic;
using Q1_Finance.Accounts;
using Q1_Finance.Models;
using Q1_Finance.Processors;

namespace Q1_Finance
{
    /// <summary>
    /// Demo finance application that creates an account, processors, transactions,
    /// processes them and applies successful ones to the account while storing them.
    /// </summary>
    public class FinanceApp
    {
        private readonly List<Transaction> _transactions = new();

        public void Run()
        {
            Console.WriteLine("=== Q1_Finance Demo ===\n");

            // Create account
            var account = new SavingsAccount("ACC1001", 1000.00m);
            Console.WriteLine($"Created SavingsAccount {account.AccountNumber} with balance {account.Balance:C}\n");

            // Create processors
            ITransactionProcessor bankProcessor = new BankTransferProcessor();
            ITransactionProcessor mmProcessor = new MobileMoneyProcessor();
            ITransactionProcessor cryptoProcessor = new CryptoWalletProcessor();

            // Create transactions
            var t1 = new Transaction(1, DateTime.Now.Date, 150.00m, "Monthly subscription", "Services");
            var t2 = new Transaction(2, DateTime.Now.Date, 200.50m, "Groceries", "Food");
            var t3 = new Transaction(3, DateTime.Now.Date, 900.00m, "Phone purchase", "Electronics");
            var t4 = new Transaction(4, DateTime.Now.Date, 50.00m, "Crypto transfer", "Crypto");

            var txs = new[] { t1, t2, t3, t4 };

            // Process & apply
            foreach (var tx in txs)
            {
                // Choose processor by category
                var processor = ChooseProcessor(tx.Category, bankProcessor, mmProcessor, cryptoProcessor);
                processor.Process(tx);

                // Apply to account and only store successful transactions
                var applied = account.ApplyTransaction(tx);
                if (applied)
                {
                    _transactions.Add(tx);
                }
            }

            // Summary
            Console.WriteLine("\n--- Summary ---");
            Console.WriteLine($"Final account balance: {account.Balance:C}");
            Console.WriteLine("Stored (applied) transactions:");
            foreach (var tx in _transactions)
            {
                Console.WriteLine($" - #{tx.Id} {tx.Date:yyyy-MM-dd} | {tx.Description} | {tx.Amount:C} | {tx.Category}");
            }

            Console.WriteLine("\nDemo complete.");
        }

        private static ITransactionProcessor ChooseProcessor(string category, ITransactionProcessor bank, ITransactionProcessor mobileMoney, ITransactionProcessor crypto)
        {
            if (string.IsNullOrWhiteSpace(category)) return bank;
            var cat = category.Trim().ToLowerInvariant();

            return cat switch
            {
                "crypto" or "cryptocurrency" => crypto,
                "food" or "groceries" or "food & beverage" => mobileMoney,
                "services" or "utilities" or "electronics" => bank,
                _ => bank
            };
        }
    }
}
