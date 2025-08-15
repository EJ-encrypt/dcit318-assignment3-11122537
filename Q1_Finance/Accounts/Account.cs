using System;
using Q1_Finance.Models;

namespace Q1_Finance.Accounts
{
    public class Account
    {
        public string AccountNumber { get; }
        public decimal Balance { get; protected set; }

        public Account(string accountNumber, decimal initialBalance)
        {
            AccountNumber = accountNumber ?? throw new ArgumentNullException(nameof(accountNumber));
            Balance = initialBalance;
        }

        /// <summary>
        /// Attempts to apply (debit) the transaction from the account.
        /// Returns true if applied; false if declined (e.g., insufficient funds).
        /// </summary>
        public virtual bool ApplyTransaction(Transaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction.Amount < 0)
            {
                Console.WriteLine("Invalid transaction amount (negative).");
                return false;
            }

            if (transaction.Amount > Balance)
            {
                // decline
                Console.WriteLine($"Transaction {transaction.Id} declined: insufficient funds (requested {transaction.Amount:C}, available {Balance:C}).");
                return false;
            }

            Balance -= transaction.Amount;
            Console.WriteLine($"Applied {transaction.Amount:C} to account {AccountNumber}. New balance: {Balance:C}");
            return true;
        }

        /// <summary>
        /// Simple deposit helper
        /// </summary>
        public void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be positive.");
            Balance += amount;
        }
    }
}
