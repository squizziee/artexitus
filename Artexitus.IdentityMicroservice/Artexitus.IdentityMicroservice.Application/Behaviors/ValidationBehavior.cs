using Artexitus.IdentityMicroservice.Contracts.Exceptions;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Roles;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Users;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Artexitus.IdentityMicroservice.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : 
        IPipelineBehavior<TRequest, TResponse> where TRequest : class
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationBehavior(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            switch (request)
            {
                case RegisterUserCommand r:
                    FindValidator<RegisterUserCommand>().ValidateAndThrow(r);
                    break;

                case AddRoleCommand r:
                    FindValidator<AddRoleCommand>().ValidateAndThrow(r);
                    break;

                case UpdateRoleCommand r:
                    FindValidator<UpdateRoleCommand>().ValidateAndThrow(r);
                    break;

                case ChangePasswordCommand r:
                    FindValidator<ChangePasswordCommand>().ValidateAndThrow(r);
                    break;
            }

            return await next();
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
