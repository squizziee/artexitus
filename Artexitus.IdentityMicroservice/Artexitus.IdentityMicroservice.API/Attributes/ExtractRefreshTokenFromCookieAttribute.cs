using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Artexitus.IdentityMicroservice.API.Attributes
{
    public class ExtractRefreshTokenFromCookieAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var exception = new InvalidCredentialsException("Refresh token is not present in request cookies");

            if (!context.HttpContext.Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {
                throw exception;
            }

            context.ActionArguments.TryGetValue("request", out var argument);

            if (argument == null)
            {
                throw exception;
            }

            var property = argument.GetType().GetProperty("RefreshToken");

            if (property == null)
            {
                throw exception;
            }

            if (property.PropertyType != typeof(string))
            {
                throw exception;
            }

            property.SetValue(argument, refreshToken);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
