using FluentValidation;
using JobSity.Chatroom.Application.Features.Chatrooms.CreateRoom.UseCase;

namespace JobSity.Chatroom.Application.Features.Chatrooms.CreateRoom.Validators
{
    internal class CreateRoomValidator : AbstractValidator<CreateRoomInput>
    {
        private const string EmptyPropertyErrorMessage = "The property {PropertyName} cannot be null or empty.";

        public CreateRoomValidator()
        {
            RuleFor(i => i.Name)
                .NotEmpty()
                .WithMessage(EmptyPropertyErrorMessage)
                .NotNull()
                .WithMessage(EmptyPropertyErrorMessage);
        }
    }
}
