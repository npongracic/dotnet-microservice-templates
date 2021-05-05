using System;
using System.Runtime.Serialization;

namespace SC.API.CleanArchitecture.Application.Common.Exceptions
{
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException()
        {
        }

        public DuplicateItemException(string message) : base(message)
        {
        }

        public DuplicateItemException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateItemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
