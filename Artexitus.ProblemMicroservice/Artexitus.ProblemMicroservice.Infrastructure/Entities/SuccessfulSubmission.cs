namespace Artexitus.ProblemMicroservice.Infrastructure.Entities
{
    public class SuccessfulSubmission : EntityBase
    {
        public Guid SubmisionId { get; set; }
        public long ElapsedTimeInNanoseconds { get; set; }
        public long ElapsedSpaceInBytes { get; set; }
    }
}
