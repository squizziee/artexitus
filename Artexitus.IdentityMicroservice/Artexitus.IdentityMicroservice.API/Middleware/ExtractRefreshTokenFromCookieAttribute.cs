using Microsoft.AspNetCore.Mvc.Filters;

namespace Artexitus.IdentityMicroservice.API.Middleware
{
    public class ExtractRefreshTokenFromCookieAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {
                throw new Exception("??");
            }

            context.ActionArguments.TryGetValue("request", out var argument);

            if (argument == null)
            {
                throw new Exception("??");
            }

            var property = argument.GetType().GetProperty("RefreshToken");

            if (property == null)
            {
                throw new Exception("??");
            }

            if (property.PropertyType != typeof(string))
            {
                throw new Exception("??");
            }

            property.SetValue(argument, refreshToken);
        }

        public override void OnActionExecuted(ActionExecutedContext context) 
        { 
        }
    }
}
