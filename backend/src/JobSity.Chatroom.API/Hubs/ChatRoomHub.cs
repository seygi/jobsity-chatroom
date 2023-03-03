using JobSity.Chatroom.Application.Features.Chatroom.ChatroomCreateMessage.UseCase;
using JobSity.Chatroom.Application.Shared.UseCase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NetDevPack.Identity.User;

namespace JobSity.Chatroom.API.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatRoomHub : Hub
    {
        private readonly IAspNetUser _aspNetUser;
        private readonly IUseCase<CreateMessageInput, DefaultOutput> _createMessageUseCase;

        public ChatRoomHub(IAspNetUser aspNetUser, IUseCase<CreateMessageInput, DefaultOutput> createMessageUseCase)
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
