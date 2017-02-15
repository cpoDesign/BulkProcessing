using System;
using System.Runtime.Serialization;

namespace Tester.Actors
{
    [Serializable]
    internal class SimulatedTerribleException : Exception
    {
        public SimulatedTerribleException()
        {
        }

        public SimulatedTerribleException(String message) : base(message)
        {
        }

        public SimulatedTerribleException(String message, Exception innerException) : base(message, innerException)
        {
        }

        protected SimulatedTerribleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}