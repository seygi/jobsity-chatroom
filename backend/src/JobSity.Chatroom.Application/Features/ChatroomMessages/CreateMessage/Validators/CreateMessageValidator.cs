using FluentValidation;
using JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase;
using JobSity.Chatroom.Application.Features.Chatrooms.CreateRoom.UseCase;
using JobSity.Chatroom.Application.Shared.Validator.Validators;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.Validators
{
    internal class CreateMessageValidator : AbstractValidator<CreateMessageInput>
    {
        private const string EmptyPropertyErrorMessage = "The property {PropertyName} cannot be null or empty.";

        public CreateMessageValidator()
        {
            RuleFor(i => i.CreatedUserId)
                .NotEmpty()
                .WithMessage(EmptyPropertyErrorMessage)
                .NotNull()
                .WithMessage(EmptyPropertyErrorMessage);
            RuleFor(i => i.ChatRoomId)
                .NotEmpty()
                .WithMessage(EmptyPropertyErrorMessage)
                .NotNull()
                .WithMessage(EmptyPropertyErrorMessage);
            RuleFor(i => i.CreatedOn)
                .NotNull()
                .WithMessage(EmptyPropertyErrorMessage);
            RuleFor(i => i.CreatedUserName)
                .NotEmpty()
                .WithMessage(EmptyPropertyErrorMessage)
                .NotNull()
                .WithMessage(EmptyPropertyErrorMessage);
            RuleFor(i => i.Text)
                .NotEmpty()
                .WithMessage(EmptyPropertyErrorMessage)
                .NotNull()
                .WithMessage(EmptyPropertyErrorMessage)
                .Length(1, 255)
                .WithMessage("The {PropertyName} must have between 1 and 255 characters");
        }
    }
}
