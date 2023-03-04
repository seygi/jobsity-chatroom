using JobSity.Chatroom.Application.Shared.UseCase;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.GetMessagesRoom.UseCase
{
    public sealed class GetMessagesRoomInput : IInput
    {
        public Guid ChatRoomId { get; set; }

        public GetMessagesRoomInput(Guid chatRoomId)
        {
            ChatRoomId = chatRoomId;
        }

        public static GetMessagesRoomInput Create(Guid chatRoomId)
            => new(chatRoomId);
    }
}
