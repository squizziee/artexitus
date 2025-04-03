using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Artexitus.ProblemMicroservice.Infrastructure.Persistence
{
    public class ProblemDatabaseContext : DbContext
    {
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
