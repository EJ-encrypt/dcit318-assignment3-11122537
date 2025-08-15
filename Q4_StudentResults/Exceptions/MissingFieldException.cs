using System;

namespace Q4_StudentResults.Exceptions
{
    public class MissingFieldException : Exception
    {
        public MissingFieldException() { }
        public MissingFieldException(string message) : base(message) { }
        public MissingFieldException(string message, Exception inner) : base(message, inner) { }
    }
}
