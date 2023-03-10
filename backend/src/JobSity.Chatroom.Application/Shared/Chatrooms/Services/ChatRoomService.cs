using JobSity.Chatroom.Application.Shared.ChatroomMessages.Repositories;
using JobSity.Chatroom.Application.Shared.Chatrooms.Entities;
using JobSity.Chatroom.Application.Shared.Chatrooms.UseCases.Inputs;

namespace JobSity.Chatroom.Application.Shared.Chatrooms.Services
{
    public class ChatRoomService : IChatRoomService
    {
        private readonly IChatRoomRepository _chatRoomRepository;
        public ChatRoomService(IChatRoomRepository chatRoomRepository)
        {
            _chatRoomRepository = chatRoomRepository;
        }

        public Task<int> CreateChatRoomAsync(CreateRoomInputBase input, CancellationToken cancellationToken)
        {
            var chatRoom = new ChatRoom(input.Name, input.CreatedUserId);

            _chatRoomRepository.Add(chatRoom);

            return _chatRoomRepository.SaveChangesAsync();
        }

        public Task<IEnumerable<ChatRoom>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _chatRoomRepository.GetAllAsync();
        }
    }
}