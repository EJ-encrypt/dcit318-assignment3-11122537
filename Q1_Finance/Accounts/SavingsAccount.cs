using System;
using Q1_Finance.Models;

namespace Q1_Finance.Accounts
{
    /// <summary>
    /// SavingsAccount disallows overdraft (declines when insufficient funds).
    /// Demonstrates sealed override.
    /// </summary>
    public sealed class SavingsAccount : Account
    {
        public SavingsAccount(string accountNumber, decimal initialBalance)
            : base(accountNumber, initialBalance)
        {
        }

        public override bool ApplyTransaction(Transaction transaction)
        {
            // Example business rule: a savings account may have a minimum balance requirement.
            // For simplicity we only enforce non-overdraft here.
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction.Amount < 0)
            {
                Console.WriteLine("Invalid transaction amount (negative).");
                return false;
            }

            if (transaction.Amount > Balance)
            {
                Console.WriteLine($"SavingsAccount: Transaction {transaction.Id} declined: insufficient funds (requested {transaction.Amount:C}, available {Balance:C}).");
                return false;
            }

            // Apply by calling base
            return base.ApplyTransaction(transaction);
        }
    }
}
