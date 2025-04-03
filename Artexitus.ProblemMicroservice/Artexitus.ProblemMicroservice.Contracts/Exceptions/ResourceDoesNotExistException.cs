namespace Artexitus.ProblemMicroservice.Contracts.Exceptions
{
    public class ResourceDoesNotExistException : Exception
    {
        public ResourceDoesNotExistException()
        {
        }

        public ResourceDoesNotExistException(string message)
            : base(message)
        {
        }

        public ResourceDoesNotExistException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
