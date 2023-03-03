using NetDevPack.Domain;

namespace JobSity.Chatroom.Application.Shared.Chat
{
    public class ChatMessage : Entity, IAggregateRoot
    {
        public Guid CreatedUserId { get; set; }
        public Guid ChatRoomId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedUserName { get; set; }
        public string Text { get; set; }
        public ChatRoom ChatRoom { get; set; } = null!;

        protected ChatMessage() { }

        public ChatMessage(Guid createdUserId, Guid chatRoomId, string createdUserName, string text)
        {
            CreatedUserId = createdUserId;
            ChatRoomId = chatRoomId;
            CreatedUserName = createdUserName;
            Text = text;
        }
    }
}
