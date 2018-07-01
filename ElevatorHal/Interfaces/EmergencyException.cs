using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorHal.Interfaces
{
    /// <summary>
    /// Exception thrown when a Hardware Component has an Error
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class EmergencyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmergencyException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public EmergencyException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmergencyException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
        public EmergencyException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmergencyException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected EmergencyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}