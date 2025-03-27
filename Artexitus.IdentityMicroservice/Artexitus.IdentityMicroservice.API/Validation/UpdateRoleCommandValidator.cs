using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Roles;
using FluentValidation;

namespace Artexitus.IdentityMicroservice.API.Validation
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(r => r.Name)
               .Length(3, 100)
               .WithMessage("Invalid name length: should be between 3 and 100");

            RuleFor(r => r.Description)
                .Length(3, 1000)
                .WithMessage("Invalid description length: should be between 3 and 100");
        }
    }
}
