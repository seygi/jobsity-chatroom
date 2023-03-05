using JobSity.Chatroom.Application.Shared.ChatroomMessages.Entities;
using JobSity.Chatroom.Application.Shared.UseCase;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.SyncBotMessages.UseCase
{
    public sealed class SyncBotMessagesInput : IInput
    {
        public Func<ChatMessage, Task> Handler { get; set; }
        private SyncBotMessagesInput(Func<ChatMessage, Task> handler)
        {
            Handler = handler;
        }

        public static SyncBotMessagesInput Create(Func<ChatMessage, Task> handler) => new(handler);

        public static SyncBotMessagesInput Empty => new(default);
    }
}
