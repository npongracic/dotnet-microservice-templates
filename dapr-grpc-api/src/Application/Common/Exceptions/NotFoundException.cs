using System;

namespace SC.API.CleanArchitecture.Application.Common.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException()
            : base()
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }

        protected NotFoundException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
        }
    }
}
