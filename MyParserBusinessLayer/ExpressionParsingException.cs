using System;

namespace Oss.BuisinessLayer
{
    internal class ExpressionParsingException : Exception
    {
        public ExpressionParsingException(string message) : base(message)
        {
        }

        public ExpressionParsingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}