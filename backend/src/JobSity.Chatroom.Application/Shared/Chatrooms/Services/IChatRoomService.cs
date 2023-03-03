using JobSity.Chatroom.Application.Shared.Chatrooms.UseCases.Inputs;

namespace JobSity.Chatroom.Application.Shared.Chatrooms.Services
{
    public interface IChatRoomService
    {
        Task<int> CreateChatRoomAsync(CreateRoomInputBase input, CancellationToken cancellationToken);
    }
}