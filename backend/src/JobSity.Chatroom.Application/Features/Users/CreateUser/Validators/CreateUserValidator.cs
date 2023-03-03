using FluentValidation;
using JobSity.Chatroom.Application.Features.Users.CreateUser.UseCase;
using JobSity.Chatroom.Application.Features.Users.LoginUser.UseCase;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.Validators
{
    internal class CreateUserValidator : AbstractValidator<CreateUserInput>
    {
        private const string EmptyPropertyErrorMessage = "The property {PropertyName} cannot be null or empty.";

        public CreateUserValidator()
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
            RuleFor(i => i.ConfirmPassword)
                .Equal(i => i.Password)
                .WithMessage("The {PropertyName} must have the same Password value");
        }
    }
}
