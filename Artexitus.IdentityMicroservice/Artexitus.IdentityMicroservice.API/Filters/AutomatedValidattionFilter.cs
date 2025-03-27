using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Roles;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Users;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Artexitus.IdentityMicroservice.API.Filters
{
    public class AutomatedValidattionFilter : IActionFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public AutomatedValidattionFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
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
                    FindValidator<RegisterUserCommand>().ValidateAndThrow(request);
                    break;

                case AddRoleCommand request:
                    FindValidator<AddRoleCommand>().ValidateAndThrow(request);
                    break;

                case UpdateRoleCommand request:
                    FindValidator<UpdateRoleCommand>().ValidateAndThrow(request);
                    break;

                case ChangePasswordCommand request:
                    FindValidator<ChangePasswordCommand>().ValidateAndThrow(request);
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

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
