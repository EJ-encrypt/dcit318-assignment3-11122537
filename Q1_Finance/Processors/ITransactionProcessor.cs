using Q1_Finance.Models;

namespace Q1_Finance.Processors
{
    public interface ITransactionProcessor
    {
        void Process(Transaction transaction);
    }
}
