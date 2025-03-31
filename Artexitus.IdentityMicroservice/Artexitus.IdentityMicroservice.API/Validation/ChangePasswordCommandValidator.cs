using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Users;
using FluentValidation;

namespace Artexitus.IdentityMicroservice.API.Validation
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(r => r.NewPassword)
                .Matches("^.*(?=.{8,})(?=.*[a-zA-Z])(?=.*\\d)(?=.*[!#$%&? \"]).*$")
                .WithMessage("Incorrect password size: should be between 8 and 100");

            RuleFor(r => r.NewPassword)
                .Length(8, 100)
                .WithMessage("Incorrect password size: should be between 8 and 100");
        }
    }
}
