namespace Artexitus.IdentityMicroservice.Contracts.Exceptions
{
    public class UnacceptableCommandException : Exception
    {
        public UnacceptableCommandException()
        {
        }

        public UnacceptableCommandException(string message)
            : base(message)
        {
        }

        public UnacceptableCommandException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
