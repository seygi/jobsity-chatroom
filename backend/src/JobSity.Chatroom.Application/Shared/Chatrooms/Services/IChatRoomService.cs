using JobSity.Chatroom.Application.Shared.Chatrooms.Entities;
using JobSity.Chatroom.Application.Shared.Chatrooms.UseCases.Inputs;

namespace JobSity.Chatroom.Application.Shared.Chatrooms.Services
{
    public interface IChatRoomService
    {
        Task<int> CreateChatRoomAsync(CreateRoomInputBase input, CancellationToken cancellationToken);
        Task<IEnumerable<ChatRoom>> GetAllAsync(CancellationToken cancellationToken);
    }
}