using FluentValidation;
using JobSity.Chatroom.Application.Features.ChatroomMessages.GetMessagesRoom.UseCase;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.GetMessagesRoom.Validators
{
    internal class GetMessagesRoomValidator : AbstractValidator<GetMessagesRoomInput>
    {
        private const string EmptyPropertyErrorMessage = "The property {PropertyName} cannot be null or empty.";

        public GetMessagesRoomValidator()
        {
            RuleFor(i => i.ChatRoomId)
                .NotEmpty()
                .WithMessage(EmptyPropertyErrorMessage)
                .NotNull()
                .WithMessage(EmptyPropertyErrorMessage);
        }
    }
}
