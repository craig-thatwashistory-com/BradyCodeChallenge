using System;

namespace BradyCodeChallengeCore.Exceptions
{

    public class ErrorInConfigXMLException : Exception
    {
        public ErrorInConfigXMLException()
        {
        }

        public ErrorInConfigXMLException(string message)
            : base(message)
        {
        }

        public ErrorInConfigXMLException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class ErrorInReferenceXMLException : Exception
    {
        public ErrorInReferenceXMLException()
        {
        }

        public ErrorInReferenceXMLException(string message)
            : base(message)
        {
        }

        public ErrorInReferenceXMLException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class ErrorInGeneratorInputXMLException : Exception
    {
        public ErrorInGeneratorInputXMLException()
        {
        }

        public ErrorInGeneratorInputXMLException(string message)
            : base(message)
        {
        }

        public ErrorInGeneratorInputXMLException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}

