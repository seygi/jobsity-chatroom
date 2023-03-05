using JobSity.Chatroom.Application.Shared.UseCase;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.SyncBotMessages.UseCase
{
    public sealed class SyncBotMessagesOutput : IOutput
    {
        public bool Success { get; }

        private SyncBotMessagesOutput(bool success)
        {
            Success = success;
        }

        public static SyncBotMessagesOutput Create(bool success) => new(success);

        public static SyncBotMessagesOutput Empty => new(false);
    }
}
