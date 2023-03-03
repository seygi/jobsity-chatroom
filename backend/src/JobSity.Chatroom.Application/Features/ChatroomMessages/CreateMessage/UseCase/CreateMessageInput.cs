using JobSity.Chatroom.Application.Shared.ChatroomMessages.UseCases.Inputs;
using JobSity.Chatroom.Application.Shared.UseCase;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase
{
    public sealed class CreateMessageInput : CreateMessageInputBase, IInput
    {
        public CreateMessageInput(Guid createdUserId, Guid chatRoomId, string createdUserName, string text)
            : base(createdUserId, chatRoomId, createdUserName, text)
        {

        }

        public static CreateMessageInput Create(Guid createdUserId, Guid chatRoomId, string createdUserName, string text)
            => new(createdUserId, chatRoomId, createdUserName, text);
    }
}
