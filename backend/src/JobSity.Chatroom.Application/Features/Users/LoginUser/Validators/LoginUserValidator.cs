using FluentValidation;
using JobSity.Chatroom.Application.Features.Users.LoginUser.UseCase;

namespace JobSity.Chatroom.Application.Features.Users.LoginUser.Validators
{
    internal class LoginUserValidator : AbstractValidator<LoginUserInput>
    {
        private const string EmptyPropertyErrorMessage = "The property {PropertyName} cannot be null or empty.";

        public LoginUserValidator()
        {
            RuleFor(i => i.Email)
                .NotEmpty()
                .WithMessage(EmptyPropertyErrorMessage)
                .NotNull()
                .WithMessage(EmptyPropertyErrorMessage)
                .EmailAddress();
            RuleFor(i => i.Password)
                .NotEmpty()
                .WithMessage(EmptyPropertyErrorMessage)
                .NotNull()
                .WithMessage(EmptyPropertyErrorMessage)
                .Length(6, 100)
                .WithMessage("The {PropertyName} must have above 6 characters");
        }
    }
}
