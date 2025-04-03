namespace Artexitus.ProblemMicroservice.Infrastructure.Entities
{
    /*
    Many-to-many table because it is only fair to compare submissions
    if they are written in the same programming language. Statistics structure
    is pretty starightforward: max and min values of time/space are calculated
    and the whole collection of succesful submissions is divided in N parts between those.

    Example:

    Max time elapsed = 1000ns
    Min time elapsed = 50ns
    N = 25
    Range of each bin (R) = (1000 - 50) / 25 = 38

    Bins: (50, 88), (89, 126), (127, 164) ... (963, 1000)
    General: (min, min + R), (min + R + 1, min + 2R) ... (min + NR + 1, max)

    Each part has the count of submission in such range, the percentage of this count
    from the whole collection of successful submissions and the sample of submission.
     */
    public class ProblemStatistics
    {
        public Guid ProblemId { get; set; }
        public Problem Problem { get; set; }
        public Guid LanguageId { get; set; }
        public ProgrammingLanguage Language { get; set; }
        public int SuccessfulSubmissionCount { get; set; }
        public int TotalSubmissionCount { get; set; }

        // Both are supposed to be stored in JSON format because they are never referenced
        // in other entities
        public IEnumerable<ProblemStatisticsBin> DistributionByTimeElapsed { get; set; } = [];
        public IEnumerable<ProblemStatisticsBin> DistributionBySpaceElapsed { get; set; } = [];
    }

    public class ProblemStatisticsBin
    {
        public double PercentageOfSubmissions { get; set; }
        public int CountOfSubmissions { get; set; }
        public long RangeUpperBound { get; set; }
        public long RangeLowerBound { get; set; }
        public Guid SubmissionSampleId { get; set; }
    }
}
