using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Artexitus.IdentityMicroservice.API.Middleware
{
    public class ExtractRefreshTokenFromCookieAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {
                throw new InvalidCredentialsException("Access token is not present in request cookies");
            }

            context.ActionArguments.TryGetValue("request", out var argument);

            if (argument == null)
            {
                throw new InvalidCredentialsException("Access token is not present in request cookies");
            }

            var property = argument.GetType().GetProperty("RefreshToken");

            if (property == null)
            {
                throw new InvalidCredentialsException("Access token is not present in request cookies");
            }

            if (property.PropertyType != typeof(string))
            {
                throw new InvalidCredentialsException("Access token is not present in request cookies");
            }

            property.SetValue(argument, refreshToken);
        }

        public override void OnActionExecuted(ActionExecutedContext context) 
        { 
        }
    }
}
