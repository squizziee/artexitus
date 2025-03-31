using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace Artexitus.IdentityMicroservice.API.Filters
{
    public class DashboardFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            return httpContext.User.Identity?.IsAuthenticated ?? false &&
                httpContext.User.IsInRole("Admin");
        }
    }
}
