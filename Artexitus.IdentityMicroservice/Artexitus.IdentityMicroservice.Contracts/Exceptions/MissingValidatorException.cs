namespace Artexitus.IdentityMicroservice.Contracts.Exceptions
{
    public class MissingValidatorException : Exception
    {
        public MissingValidatorException()
        {
        }

        public MissingValidatorException(string message)
            : base(message)
        {
        }

        public MissingValidatorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
