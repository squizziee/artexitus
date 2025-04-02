using Artexitus.ProblemMicroservice.Infrastructure.Enums;

namespace Artexitus.ProblemMicroservice.Infrastructure.Entities
{
    public class Problem : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string MarkdownDescription { get; set; } = string.Empty;
        public int SequenceNumber { get; set; }
        public ProblemDifficulty Difficulty { get; set; }
        // statictics there
    }
}
