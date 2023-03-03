using JobSity.Chatroom.Application.Shared.Chatrooms.UseCases.Inputs;
using JobSity.Chatroom.Application.Shared.UseCase;

namespace JobSity.Chatroom.Application.Features.Chatrooms.CreateRoom.UseCase
{
    public sealed class CreateRoomInput : CreateRoomInputBase, IInput
    {
        public CreateRoomInput(string name, Guid createdUserId)
            : base(name, createdUserId) 
        {
        }

        public static CreateRoomInput Create(string name)
            => new(name, Guid.Empty);
    }
}
