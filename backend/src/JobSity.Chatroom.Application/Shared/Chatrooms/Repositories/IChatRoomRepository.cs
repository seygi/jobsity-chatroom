using JobSity.Chatroom.Application.Shared.ChatroomMessages.Entities;
using JobSity.Chatroom.Application.Shared.Chatrooms.Entities;

namespace JobSity.Chatroom.Application.Shared.ChatroomMessages.Repositories
{
    public interface IChatRoomRepository
    {
        //Task<ChatMessage> GetById(Guid id);
        Task<IEnumerable<ChatRoom>> GetAllAsync();
        //Task<IEnumerable<ChatMessage>> GetAllByChatRoomId(Guid chatRoomId);
        void Add(ChatRoom message);
        Task<int> SaveChangesAsync();
    }
}