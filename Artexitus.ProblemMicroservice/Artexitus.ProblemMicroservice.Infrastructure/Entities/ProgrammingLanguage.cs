namespace Artexitus.ProblemMicroservice.Infrastructure.Entities
{
    public class ProgrammingLanguage : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IEnumerable<Problem> ProblemsWithSupport { get; set; } = [];
        public IEnumerable<Problem> ProblemsWithStatistics { get; set; } = [];
        public IEnumerable<Submission> Submissions { get; set; } = [];
    }
}
