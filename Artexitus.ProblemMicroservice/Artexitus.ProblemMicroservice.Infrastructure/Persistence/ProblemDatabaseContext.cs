using Artexitus.ProblemMicroservice.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Artexitus.ProblemMicroservice.Infrastructure.Persistence
{
    public class ProblemDatabaseContext : DbContext
    {
        public DbSet<Problem> Problems { get; set; }
        public DbSet<ProgrammingLanguage> Languages { get; set; }
        public DbSet<ProblemStarterCode> ProblemStarterCodeCollection { get; set; }
        public DbSet<ProblemStatistics> ProblemStatistics { get; set; }
        public DbSet<GeneralProblemStatistics> GeneralProblemStatistics { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<SuccesfulSubmission> SuccesfulSubmissions { get; set; }

        public ProblemDatabaseContext(DbContextOptions<ProblemDatabaseContext> options) 
            : base(options)
        {          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
