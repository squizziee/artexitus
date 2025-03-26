using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Artexitus.IdentityMicroservice.API.Middleware
{
    public class ExtractIdFromCookieAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var id = context.HttpContext.User.Identity?.Name;

            if (id == null)
            {
                throw new InvalidCredentialsException("Id is not present in request cookies");
            }

            context.ActionArguments.TryGetValue("request", out var argument);

            if (argument == null)
            {
                throw new InvalidCredentialsException("Id is not present in request cookies");
            }

            var property = argument.GetType().GetProperty("Id");

            if (property == null)
            {
                throw new InvalidCredentialsException("Id is not present in request cookies");
            }

            if (property.PropertyType != typeof(Guid))
            {
                throw new InvalidCredentialsException("Id is not present in request cookies");
            }

            property.SetValue(argument, Guid.Parse(id));
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
