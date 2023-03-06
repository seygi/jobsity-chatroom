using JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase;
using JobSity.Chatroom.Application.Shared.UseCase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NetDevPack.Identity.User;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.API.Hubs
{
    [ExcludeFromCodeCoverage]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatRoomHub : Hub
    {
        private readonly IAspNetUser _aspNetUser;
        private readonly IUseCase<CreateMessageInput, CreateMessageOutput> _createMessageUseCase;

        public ChatRoomHub(IAspNetUser aspNetUser, IUseCase<CreateMessageInput, CreateMessageOutput> createMessageUseCase)
        {
            _aspNetUser = aspNetUser;
            _createMessageUseCase = createMessageUseCase;
        }

        public async Task SendMessage(Guid chatRoomId, string text)
        {
            var message = CreateMessageInput.Create(_aspNetUser.GetUserId(), chatRoomId, _aspNetUser.GetUserEmail(), text);

            await _createMessageUseCase.ExecuteAsync(message, default);

            await Clients.All.SendAsync("ReceiveMessage", new { chatRoomId });
        }
    }
}
