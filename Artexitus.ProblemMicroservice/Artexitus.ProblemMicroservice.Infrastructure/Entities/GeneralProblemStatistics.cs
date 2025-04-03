namespace Artexitus.ProblemMicroservice.Infrastructure.Entities
{
    public class GeneralProblemStatistics : EntityBase
    {
        public Guid ProblemId { get; set; }
        public Problem Problem { get; set; }
        public int SuccessfulSubmissionCount { get; set; }
        public int TotalSubmissionCount { get; set; }
    }
}
