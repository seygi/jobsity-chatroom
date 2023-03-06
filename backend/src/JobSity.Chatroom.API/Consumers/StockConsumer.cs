using JobSity.Chatroom.API.Hubs;
using JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase;
using JobSity.Chatroom.Application.Features.ChatroomMessages.SyncBotMessages.UseCase;
using JobSity.Chatroom.Application.Shared.ChatroomMessages.Entities;
using JobSity.Chatroom.Application.Shared.UseCase;
using Microsoft.AspNetCore.SignalR;

namespace JobSity.Chatroom.API.Consumers
{
    public class StockConsumer : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHubContext<ChatRoomHub> _hubContext;
        
        private readonly Guid _botGuid = Guid.Parse("c18d3f66-e32f-461f-8e53-843803a99694");
        private readonly string _botEmail = "bot@jobsity.com";

        public StockConsumer(IServiceScopeFactory serviceScopeFactory, IHubContext<ChatRoomHub> hubContext)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _hubContext = hubContext;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _syncMessagesUseCase = scope.ServiceProvider.GetService<IUseCase<SyncBotMessagesInput, SyncBotMessagesOutput>>();
                var sendToHub = SyncBotMessagesInput.Create(HandleMessageReceivedAsync);
                await _syncMessagesUseCase.ExecuteAsync(sendToHub, CancellationToken.None);
            }
        }

        public async Task HandleMessageReceivedAsync(ChatMessage message)
        {
            var createMessageInput = CreateMessageInput.Create(_botGuid, message.ChatRoomId, _botEmail, message.Text);

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _createMessagesUseCase = scope.ServiceProvider.GetService<IUseCase<CreateMessageInput, CreateMessageOutput>>();
                await _createMessagesUseCase.ExecuteAsync(createMessageInput, default);
            }

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", new { message.ChatRoomId });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
