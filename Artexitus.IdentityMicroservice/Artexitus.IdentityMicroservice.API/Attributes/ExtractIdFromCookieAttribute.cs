using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Artexitus.IdentityMicroservice.API.Attributes
{
    public class ExtractIdFromCookieAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var id = context.HttpContext.User.Identity?.Name;
            var exception = new InvalidCredentialsException("Id is not present in request cookies");

            if (id == null)
            {
                throw exception;
            }

            context.ActionArguments.TryGetValue("request", out var argument);

            if (argument == null)
            {
                throw exception;
            }

            var property = argument.GetType().GetProperty("Id");

            if (property == null)
            {
                throw exception;
            }

            if (property.PropertyType != typeof(Guid))
            {
                throw exception;
            }

            property.SetValue(argument, Guid.Parse(id));
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
