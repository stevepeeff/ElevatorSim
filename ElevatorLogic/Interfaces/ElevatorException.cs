using System;
using System.Runtime.Serialization;

namespace ElevatorLogic.Interfaces
{
    public class ElevatorException : Exception
    {
        public ElevatorException(string message) : base(message)
        {
        }

        public ElevatorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ElevatorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}