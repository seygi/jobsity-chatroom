using JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase;
using JobSity.Chatroom.Application.Features.ChatroomMessages.SyncBotMessages.UseCase;
using JobSity.Chatroom.Application.Shared.Identity;
using JobSity.Chatroom.Application.Shared.UseCase;
using Microsoft.AspNetCore.SignalR.Client;

using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;

namespace JobSity.Chatroom.Consumer.BotStockSearch
{
    public class Consumer : IHostedService
    {
        private readonly string _botToken;
        private readonly IUseCase<DefaultInput, SearchStockOutput> _stockValueuseCase;
        private readonly IUseCase<SyncBotMessagesInput, SyncBotMessagesOutput> _syncMessagesUseCase;
        private HubConnection _hubConnection;

        public Consumer(IUseCase<DefaultInput, SearchStockOutput> stockValueuseCase, IUseCase<SyncBotMessagesInput, SyncBotMessagesOutput> syncMessagesUseCase, ITokenService tokenService)
        {
            _stockValueuseCase = stockValueuseCase;
            _syncMessagesUseCase = syncMessagesUseCase;
            _botToken = tokenService.GenerateBotToken();

            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"https://localhost:7154/chat-room-hub/?token={_botToken}", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(_botToken);
                })
                .Build();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _hubConnection.StartAsync();
            _stockValueuseCase.ExecuteAsync(DefaultInput.Default, CancellationToken.None);

            var sendToHub = SyncBotMessagesInput.Create(async (qMsg) => {
                await _hubConnection.InvokeAsync("SendMessage", qMsg.ChatRoomId, qMsg.Text);
            });

            _syncMessagesUseCase.ExecuteAsync(sendToHub, CancellationToken.None);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
