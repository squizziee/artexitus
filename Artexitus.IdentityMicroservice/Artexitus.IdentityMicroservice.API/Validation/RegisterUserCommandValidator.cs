using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Users;
using FluentValidation;

namespace Artexitus.IdentityMicroservice.API.Validation
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("Email address cannot be empty");

            RuleFor(r => r.Email)
                .EmailAddress()
                .WithMessage("Incorrect email address format");

            RuleFor(r => r.Username)
                .Matches("^[A-Za-z][A-Za-z0-9]*$")
                .WithMessage("Incorrect username format");

            RuleFor(r => r.Username)
                .Length(3, 50)
                .WithMessage("Incorrect username size: should be between 3 and 50");

            RuleFor(r => r.Password)
                .Matches("^.*(?=.{8,})(?=.*[a-zA-Z])(?=.*\\d)(?=.*[!#$%&? \"]).*$")
                .WithMessage("Incorrect password size: should be between 8 and 100");

            RuleFor(r => r.Password)
                .Length(8, 100)
                .WithMessage("Incorrect password size: should be between 8 and 100");
        }
    }
}
