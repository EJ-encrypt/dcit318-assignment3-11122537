using System;

namespace Q3_Warehouse.Exceptions
{
    public class InvalidQuantityException : Exception
    {
        public InvalidQuantityException() { }
        public InvalidQuantityException(string message) : base(message) { }
        public InvalidQuantityException(string message, Exception inner) : base(message, inner) { }
    }
}
