namespace JobSity.Chatroom.Application.Features.ChatroomMessages.GetMessagesRoom.UseCase
{
    public class GetMessagesRoomOutput
    {
        public Guid CreatedUserId { get; set; }
        public Guid ChatRoomId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedUserName { get; set; }
        public string Text { get; set; }
        private GetMessagesRoomOutput(Guid createdUserId, Guid chatRoomId, DateTime createdOn, string createdUserName, string text)
        {
            CreatedUserId = createdUserId;
            ChatRoomId = chatRoomId;
            CreatedOn = createdOn;
            CreatedUserName = createdUserName;
            Text = text;
        }

        public static GetMessagesRoomOutput Create(Guid createdUserId, Guid chatRoomId, DateTime createdOn, string createdUserName, string text) =>
            new(createdUserId, chatRoomId, createdOn, createdUserName, text);
    }
}