using XpInc.Security.FacialBiometrics.Application.Shared.Users.UseCases.Inputs;

namespace JobSity.Chatroom.Application.Features.Chatroom.ChatroomCreateMessage.UseCase
{
    public sealed class CreateMessageInput : CreateMessageInputBase
    {
        public CreateMessageInput(Guid createdUserId, Guid chatRoomId, DateTime createdOn, string createdUserName, string text)
            : base(createdUserId, chatRoomId, createdOn, createdUserName, text)
        {

        }

        public static CreateMessageInput Create(Guid createdUserId, Guid chatRoomId, DateTime createdOn, string createdUserName, string text)
            => new(createdUserId, chatRoomId, createdOn, createdUserName, text);
    }
}
