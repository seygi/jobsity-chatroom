using JobSity.Chatroom.Application.Shared.ChatroomMessages.Entities;

namespace JobSity.Chatroom.Application.Shared.ChatroomMessages.Repositories
{
    public interface IChatMessageRepository
    {
        Task<IEnumerable<ChatMessage>> GetTop50ByChatRoomId(Guid chatRoomId);
        void Add(ChatMessage message);
        Task<int> SaveChangesAsync();
    }
}