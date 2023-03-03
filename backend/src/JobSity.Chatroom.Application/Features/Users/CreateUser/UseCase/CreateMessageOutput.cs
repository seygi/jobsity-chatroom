using JobSity.Chatroom.Application.Shared.UseCase;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase
{
    public sealed class CreateUserOutput : IOutput
    {
        public bool Success { get; }

        private CreateUserOutput(bool success)
        {
            Success = success;
        }

        public static CreateUserOutput Create(bool success) => new(success);

        public static CreateUserOutput Empty => new(false);
    }
}
