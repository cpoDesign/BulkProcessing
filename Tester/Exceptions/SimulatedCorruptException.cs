using System;
using System.Runtime.Serialization;

namespace Tester.Actors
{
    [Serializable]
    internal class SimulatedCorruptException : Exception
    {
        public SimulatedCorruptException()
        {
        }

        public SimulatedCorruptException(String message) : base(message)
        {
        }

        public SimulatedCorruptException(String message, Exception innerException) : base(message, innerException)
        {
        }

        protected SimulatedCorruptException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}