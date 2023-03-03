using JobSity.Chatroom.Application.Shared.ChatroomMessages.Repositories;
using JobSity.Chatroom.Application.Shared.Chatrooms.Entities;
using JobSity.Chatroom.Application.Shared.Chatrooms.UseCases.Inputs;
using NetDevPack.Messaging;

namespace JobSity.Chatroom.Application.Shared.Chatrooms.Services
{
    public class ChatRoomService : CommandHandler, IChatRoomService
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
    }
}