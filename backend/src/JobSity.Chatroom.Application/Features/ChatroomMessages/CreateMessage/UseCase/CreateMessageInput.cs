using XpInc.Security.FacialBiometrics.Application.Shared.Users.UseCases.Inputs;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase
{
    public sealed class CreateMessageInput : CreateMessageInputBase
    {
        public CreateMessageInput(Guid createdUserId, Guid chatRoomId, string createdUserName, string text)
            : base(createdUserId, chatRoomId, createdUserName, text)
        {

        }

        public static CreateMessageInput Create(Guid createdUserId, Guid chatRoomId, string createdUserName, string text)
            => new(createdUserId, chatRoomId, createdUserName, text);
    }
}
