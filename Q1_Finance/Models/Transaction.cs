using System;

namespace Q1_Finance.Models
{
    public record Transaction(int Id, DateTime Date, decimal Amount, string Description, string Category);
}
