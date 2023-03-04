using NetDevPack.Messaging;

namespace JobSity.Chatroom.Application.Shared.Events.ChatMessage
{
    public class ChatMessageCreatedEvent : Event
    {
        public Guid Id { get; set; }
        public Guid CreatedUserId { get; set; }
        public string Text { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public ChatMessageCreatedEvent(Guid id, Guid createdUserId, string text, DateTime createdOn)
        {
            Id = id;
            AggregateId = id;
            CreatedUserId = createdUserId;
            Text = text;
            CreatedOn = createdOn;
        }
    }
}