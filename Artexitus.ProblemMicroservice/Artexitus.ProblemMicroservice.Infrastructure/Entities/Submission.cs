namespace Artexitus.ProblemMicroservice.Infrastructure.Entities
{
    public class Submission : EntityBase
    {
        public Guid UserId { get; set; }
        public Guid ProblemId { get; set; }
        public Problem Problem { get; set; }
        public Guid LanguageId { get; set; }
        public ProgrammingLanguage Language { get; set; }
        public string SourceCode { get; set; } = string.Empty;
    }
}
