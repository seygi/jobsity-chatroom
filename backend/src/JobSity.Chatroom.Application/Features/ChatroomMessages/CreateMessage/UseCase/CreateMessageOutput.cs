using JobSity.Chatroom.Application.Shared.UseCase;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase
{
    public sealed class CreateMessageOutput : IOutput
    {
        public bool Success { get; }

        private CreateMessageOutput(bool success)
        {
            Success = success;
        }

        public static CreateMessageOutput Create(bool success) => new(success);

        public static CreateMessageOutput Empty => new(false);
    }
}
