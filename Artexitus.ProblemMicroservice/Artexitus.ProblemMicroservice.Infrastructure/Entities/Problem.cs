using Artexitus.ProblemMicroservice.Infrastructure.Enums;

namespace Artexitus.ProblemMicroservice.Infrastructure.Entities
{
    public class Problem : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string MarkdownDescription { get; set; } = string.Empty;
        public int SequenceNumber { get; set; }
        public ProblemDifficulty Difficulty { get; set; }
        public IEnumerable<ProgrammingLanguage> SupportedLanguages { get; set; } = [];
        public GeneralProblemStatistics GeneralStatistics { get; set; }
        public IEnumerable<ProgrammingLanguage> Statistics { get; set; } = [];
        public IEnumerable<Submission> Submissions { get; set; } = [];
        public IEnumerable<SuccesfulSubmission> SuccessfulSubmissions { get; set; } = [];
    }
}
