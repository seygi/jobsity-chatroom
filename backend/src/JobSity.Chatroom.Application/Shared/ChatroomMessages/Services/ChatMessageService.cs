using JobSity.Chatroom.Application.Shared.ChatroomMessages.Entities;
using JobSity.Chatroom.Application.Shared.ChatroomMessages.Repositories;
using JobSity.Chatroom.Application.Shared.ChatroomMessages.UseCases.Inputs;
using JobSity.Chatroom.Application.Shared.Messaging;
using NetDevPack.Messaging;

namespace JobSity.Chatroom.Application.Shared.ChatroomMessages.Services
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly string _messagesQueueName = "messagesQueue";
        private readonly IBus _bus;

        public ChatMessageService(IChatMessageRepository chatMessageRepository, IBus bus)
        {
            _chatMessageRepository = chatMessageRepository;
            _bus = bus;
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

        public Task EnqueueMessageToInsertAsync(ChatMessage message, CancellationToken cancellationToken)
        {
            return _bus.PublishAsync(message, _messagesQueueName);
        }

        public async Task SubscribeAsync(Func<ChatMessage, Task> handler)
        {
            await _bus.SubscribeAsync(handler, _messagesQueueName);
        }
    }
}