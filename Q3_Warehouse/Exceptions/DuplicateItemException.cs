using System;

namespace Q3_Warehouse.Exceptions
{
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException() { }
        public DuplicateItemException(string message) : base(message) { }
        public DuplicateItemException(string message, Exception inner) : base(message, inner) { }
    }
}
