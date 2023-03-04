using JobSity.Chatroom.Application.Shared.UseCase;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.GetMessagesRoom.UseCase
{
    public sealed class GetMessagesRoomInput : IInput
    {
        public Guid ChatRoomId { get; set; }
        public DateTime? LastMessageTime { get; set; }

        public GetMessagesRoomInput(Guid chatRoomId, DateTime? lastMessageTime)
        {
            ChatRoomId = chatRoomId;
            LastMessageTime = lastMessageTime;
        }

        public static GetMessagesRoomInput Create(Guid chatRoomId, DateTime? lastMessageTime)
            => new(chatRoomId, lastMessageTime);
    }
}
