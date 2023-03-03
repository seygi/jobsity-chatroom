namespace JobSity.Chatroom.Application.Shared.ChatroomMessages.ViewModels
{
    public class ChatMessageViewModel
    {
        public Guid CreatedUserId { get; set; }
        public Guid ChatRoomId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedUserName { get; set; }
        public string Text { get; set; }
    }
}
