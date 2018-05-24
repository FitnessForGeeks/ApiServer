using System;
using System.Runtime.Serialization;

namespace FitnessForGeeksWebApi.Database
{
    [Serializable]
    internal class ResultIsEmptyException : Exception
    {
        public ResultIsEmptyException()
        {
        }

        public ResultIsEmptyException(string message) : base(message)
        {
        }

        public ResultIsEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ResultIsEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}