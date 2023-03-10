using JobSity.Chatroom.Application.Shared.ChatroomMessages.Entities;
using NetDevPack.Domain;

namespace JobSity.Chatroom.Application.Shared.Chatrooms.Entities
{
    public class ChatRoom : Entity, IAggregateRoot
    {
        public string Name { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public Guid CreatedUserId { get; set; }
        public virtual List<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
        protected ChatRoom() { }
        public ChatRoom(string name, Guid createdUserId)
        {
            Name = name;
            CreatedUserId = createdUserId;
        }
    }
}
