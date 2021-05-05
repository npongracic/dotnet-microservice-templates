using System;
using System.Runtime.Serialization;

namespace SC.API.CleanArchitecture.Application.Common.Exceptions
{
    public class ConsumeEventException : Exception
    {
        public ConsumeEventException()
        {
        }

        public ConsumeEventException(string message) : base(message)
        {
        }

        public ConsumeEventException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConsumeEventException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
