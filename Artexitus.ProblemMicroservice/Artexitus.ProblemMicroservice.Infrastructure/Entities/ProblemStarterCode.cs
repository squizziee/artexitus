namespace Artexitus.ProblemMicroservice.Infrastructure.Entities
{
    public class ProblemStarterCode
    {
        public Guid ProblemId { get; set; }
        public Problem Problem { get; set; }
        public Guid LanguageId { get; set; }
        public ProgrammingLanguage Language { get; set; }
    }
}
