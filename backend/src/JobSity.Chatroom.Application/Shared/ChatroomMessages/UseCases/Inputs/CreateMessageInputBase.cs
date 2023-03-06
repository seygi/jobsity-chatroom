using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.Application.Shared.ChatroomMessages.UseCases.Inputs
{
    [ExcludeFromCodeCoverage]
    public abstract class CreateMessageInputBase
    {
        public Guid CreatedUserId { get; set; }
        public Guid ChatRoomId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedUserName { get; set; }
        public string Text { get; set; }
        public CreateMessageInputBase(Guid createdUserId, Guid chatRoomId, string createdUserName, string text)
        {
            CreatedUserId = createdUserId;
            ChatRoomId = chatRoomId;
            CreatedUserName = createdUserName;
            Text = text;
        }
    }
}