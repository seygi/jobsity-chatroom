using JobSity.Chatroom.Application.Shared.ChatroomMessages.Services;
using JobSity.Chatroom.Application.Shared.UseCase;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.SyncBotMessages.UseCase
{
    internal sealed class SyncBotMessagesUseCase : IUseCase<SyncBotMessagesInput, SyncBotMessagesOutput>
    {
        private readonly IChatMessageService _chatMessageService;

        public SyncBotMessagesUseCase(IChatMessageService chatMessageService)
        {
            _chatMessageService = chatMessageService;
        }

        public async Task<SyncBotMessagesOutput> ExecuteAsync(SyncBotMessagesInput input, CancellationToken cancellationToken)
        {
            await _chatMessageService.SubscribeAsync((qMsg) =>
            {
                input.Handler(qMsg);
                return Task.CompletedTask;
            });

            return SyncBotMessagesOutput.Empty;
        }
    }
}
