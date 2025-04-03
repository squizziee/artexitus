namespace Artexitus.ProblemMicroservice.Infrastructure.Entities
{
    public class SuccesfulSubmission : EntityBase
    {
        public Guid SubmissionId { get; set; }
        public Submission Submission { get; set; }
        public long ElapsedTimeInNanoseconds { get; set; }
        public long ElapsedSpaceInBytes { get; set; }
    }
}
