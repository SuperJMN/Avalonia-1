namespace OmniXamlV1
{
    using System;

    public class ParseException : Exception
    {
        public ParseException(string message) : base(message)
        {
        }

        // ReSharper disable once UnusedMember.Global
        public ParseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}