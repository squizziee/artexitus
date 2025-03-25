using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Artexitus.IdentityMicroservice.API.Middleware
{
    public class ValidateFilter : IActionFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidateFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {          
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("request", out var argument);

            if (argument == null)
            {
                return;
            }

            switch (argument)
            {
                case RegisterUserCommand request:
                    {
                        var validator = FindValidator<RegisterUserCommand>();
                        validator.ValidateAndThrow(request);
                    }
                    
                    break;

                case ActivateAccountCommand request:
                    {
                        var validator = FindValidator<ActivateAccountCommand>();
                        validator.ValidateAndThrow(request); 
                    }
                    break;
            }
        }
        private IValidator<T> FindValidator<T>()
        {
            var validator = _serviceProvider.GetService<IValidator<T>>();

            if (validator == null)
            {
                throw new MissingValidatorException($"Validator for type {typeof(T)} was not defined. Unable to validate");
            }

            return validator;
        }
    }
}
