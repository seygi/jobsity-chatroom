using JobSity.Chatroom.Application.Shared.ChatroomMessages.Entities;
using JobSity.Chatroom.Application.Shared.ChatroomMessages.Repositories;
using JobSity.Chatroom.Application.Shared.ChatroomMessages.UseCases.Inputs;
using NetDevPack.Messaging;

namespace JobSity.Chatroom.Application.Shared.ChatroomMessages.Services
{
    public class ChatMessageService : CommandHandler, IChatMessageService
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        public ChatMessageService(IChatMessageRepository chatMessageRepository)
        {
            _chatMessageRepository = chatMessageRepository;
        }

        public Task<int> CreateMessageAsync(CreateMessageInputBase input, CancellationToken cancellationToken)
        {
            var chatMessage = new ChatMessage(input.CreatedUserId, input.ChatRoomId, input.CreatedUserName, input.Text);

            _chatMessageRepository.Add(chatMessage);

            return _chatMessageRepository.SaveChangesAsync();
        }
        public Task<IEnumerable<ChatMessage>> GetTop50ByChatRoomId(Guid chatRoomId, CancellationToken cancellationToken)
        {
            return _chatMessageRepository.GetTop50ByChatRoomId(chatRoomId);
        }
    }
}